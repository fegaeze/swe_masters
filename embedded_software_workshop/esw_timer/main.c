/**
 * @brief Example usage of Timer peripheral.
 *
 * EFR32 Application Note on Timers
 * https://www.silabs.com/documents/public/application-notes/AN0014.pdf
 *
 * EFR32MG12 Wireless Gecko Reference Manual (Timer p672)
 * https://www.silabs.com/documents/public/reference-manuals/efr32xg12-rm.pdf
 *
 * Timer API documentation 
 * https://docs.silabs.com/mcu/latest/efr32mg12/group-TIMER
 * 
 * ARM RTOS API
 * https://arm-software.github.io/CMSIS_5/RTOS2/html/group__CMSIS__RTOS.html
 * 
 * Copyright Thinnect Inc. 2019
 * Copyright ProLab TTÜ 2022
 * @license MIT
 * @author Chioma Nkem-Eze
 */
#include <stdio.h>
#include <stdint.h>
#include <string.h>
#include <inttypes.h>

#include "retargetserial.h"
#include "cmsis_os2.h"
#include "platform.h"

#include "SignatureArea.h"
#include "DeviceSignature.h"

#include "loggers_ext.h"
#include "logger_fwrite.h"

#include "timer_handler.h"

#include "loglevels.h"
#define __MODUUL__ "main"
#define __LOG_LEVEL__ (LOG_LEVEL_main & BASE_LOG_LEVEL)
#include "log.h"

// Include the information header binary
#include "incbin.h"
INCBIN(Header, "header.bin");

#define ESWGPIO_EXTI_INDEX     4
#define ESWGPIO_EXTI_IF        0x00000010UL

static uint8_t ledState = 0;

static osThreadId_t buttonThreadID;
static osThreadId_t brightnessThreadID;

const uint32_t buttonExtIntThreadFlag = 0x00000001;

static void button_loop();
static void initGPIOButton();
static void buttonIntEnable();
static void brightness_loop();


/**
 * @brief Setup and Configuration Thread. 
 * Initialize GPIO pins for LEDs and Button with config.
 * Initialize Timer Module with config
 * Creation of new thread for the led brightness functionality
 * Creation of new thread for the button interrupt loop (Only triggers from the IRQ handler)
 * Enables button interrupt
*/
void hp_loop()
{
    initialize_gpio_mod();
    initGPIOButton();

    initialize_timer_mod();

    const osThreadAttr_t button_thread_attr = { .name = "Button" };
    buttonThreadID = osThreadNew(button_loop, NULL, &button_thread_attr);

    const osThreadAttr_t brightness_thread_attr = { .name = "Brightness"};
    brightnessThreadID = osThreadNew(brightness_loop, NULL, &brightness_thread_attr);

    buttonIntEnable();
    
    for (;;);
}

/**
 * @brief Thread to handle button control. 
 * Togggles brightness thread on/off by utilizing ledState to resume/suspend the task.
 * When brightness task is suspended, provide default duty cycles for LEDs to go back to default state.
*/
void button_loop(void *args)
{
    for (;;)
    {
        osThreadFlagsClear(buttonExtIntThreadFlag);
        osThreadFlagsWait(buttonExtIntThreadFlag, osFlagsWaitAny, osWaitForever);

        if(ledState == 0) {
            ledState = 1;
            osThreadResume(brightnessThreadID);
        } else {
            ledState = 0;
            osThreadSuspend(brightnessThreadID);
            duty_cycle(TIMER_BLUE_DC, TIMER_GREEN_DC, TIMER_RED_DC);
        }    
    }
}

/**
 * @brief Thread to handle fade motion for the blue LED. 
 * When button is pressed, fade in with a duty-cycle of 0 -> 100.
 * Where 0 is 'off', and TIMER_TOP is maximum brightness
 * After 100ms, slowly fade out back to 0. The cycle repeats till thread is suspended.
*/
void brightness_loop()
{
    for(;;)
    {
        if(ledState == 1) 
        {
            duty_cycle(0, 0, 0);
            
            for (int i = 0; i < TIMER_TOP; i++)
            {
                duty_cycle(i, 0, 0);
                osDelay(20*osKernelGetTickFreq() / 1000);
            }

            osDelay(100*osKernelGetTickFreq() / 1000);

            for (int i = TIMER_TOP; i > 0; i--)
            {
                duty_cycle(i, 0, 0);
                osDelay(20*osKernelGetTickFreq() / 1000);
            }

            osDelay(50*osKernelGetTickFreq() / 1000);
        }
    }
}

int logger_fwrite_boot (const char *ptr, int len)
{
    fwrite(ptr, len, 1, stdout);
    fflush(stdout);
    return len;
}

int main ()
{
    PLATFORM_Init();

    // Configure log message output
    RETARGET_SerialInit();
    log_init(BASE_LOG_LEVEL, &logger_fwrite_boot, NULL);

    info1("ESW-Timer "VERSION_STR" (%d.%d.%d)", VERSION_MAJOR, VERSION_MINOR, VERSION_PATCH);

    // Initialize OS kernel.
    osKernelInitialize();

    // Create a thread.
    const osThreadAttr_t hp_thread_attr = { .name = "hp" };
    osThreadNew(hp_loop, NULL, &hp_thread_attr);

    if (osKernelReady == osKernelGetState())
    {
        // Switch to a thread-safe logger
        logger_fwrite_init();
        log_init(BASE_LOG_LEVEL, &logger_fwrite, NULL);

        // Start the kernel
        osKernelStart();
    }
    else
    {
        err1("!osKernelReady");
    }

    for(;;);
}

/**
  Initialize GPIO port and pin for external interrupts 
  with some default configuration.
*/
void initGPIOButton()
{
    GPIO_IntDisable(ESWGPIO_EXTI_IF);
    GPIO_ExtIntConfig(GPIO_BUTTON_PORT, GPIO_BUTTON_PIN, ESWGPIO_EXTI_INDEX, false, true, false); // Port, pin, EXTI number, rising edge, falling edge, disabled.
    GPIO_InputSenseSet(GPIO_INSENSE_INT, GPIO_INSENSE_INT);
}

/*
    The function enables the GPIO button interrupt when it is called.
    Before it does this, it clears all pending button GPIO interrupts, 
    enables the GPIO hardware interrupts (NVIC) and sets a suitable priority.
*/
void buttonIntEnable()
{
    GPIO_IntClear(ESWGPIO_EXTI_IF);
    NVIC_EnableIRQ(GPIO_EVEN_IRQn);
    NVIC_SetPriority(GPIO_EVEN_IRQn, 3);
    GPIO_IntEnable(ESWGPIO_EXTI_IF);
}

/*
    The IRQ Handler gets all pending and enabled interrupts, 
    checks specifically to see if the button interrupt is enabled, 
    the triggers the resumption of the button thread.
*/
void GPIO_EVEN_IRQHandler(void)
{
    uint32_t pending = GPIO_IntGetEnabled();

    if (pending & ESWGPIO_EXTI_IF)
    {
        GPIO_IntClear(ESWGPIO_EXTI_IF);
        osThreadFlagsSet(buttonThreadID, buttonExtIntThreadFlag);
    }
    else;
}
