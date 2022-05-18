/**
 * @brief Example usage of GPIO peripheral. Three LEDs are toggled using 
 * GPIO functionality. A hardware-to-software interrupt is set up and 
 * triggered by a button switch.
 *
 * The tsb0 board has three LEDs (red, green, blue) connected to ports
 * PB11, PB12, PA5 respectively. The button switch is connected to port
 * PF4. LED and button locations (pin and port numbers) can be found from 
 * the tsb0 board wiring schematics.
 *
 * EFR32 Application Note on GPIO
 * https://www.silabs.com/documents/public/application-notes/an0012-efm32-gpio.pdf
 *
 * EFR32MG12 Wireless Gecko Reference Manual (GPIO p1105)
 * https://www.silabs.com/documents/public/reference-manuals/efr32xg12-rm.pdf
 *
 * GPIO API documentation 
 * https://docs.silabs.com/mcu/latest/efr32mg12/group-GPIO
 * 
 * ARM RTOS API
 * https://arm-software.github.io/CMSIS_5/RTOS2/html/group__CMSIS__RTOS.html
 * 
 * Copyright Thinnect Inc. 2019
 * Copyright ProLab TTÜ 2022
 * @license MIT
 * @author Chioma NKem-Eze
 */

#include <stdio.h>
#include <stdint.h>
#include <string.h>
#include <inttypes.h>

#include "em_cmu.h"
#include "em_gpio.h"
#include "platform.h"
#include "cmsis_os2.h"
#include "retargetserial.h"

#include "SignatureArea.h"
#include "DeviceSignature.h"

#include "loggers_ext.h"
#include "logger_fwrite.h"

#include "loglevels.h"
#define __MODUUL__ "main"
#define __LOG_LEVEL__ (LOG_LEVEL_main & BASE_LOG_LEVEL)
#include "log.h"

// Include the information header binary
#include "incbin.h"
INCBIN(Header, "header.bin");

#define GPIO_BUZZER_PIN        0
#define GPIO_BUTTON_PIN        4
#define GPIO_BUZZER_PORT       gpioPortA
#define GPIO_BUTTON_PORT       gpioPortF
#define ESWGPIO_EXTI_INDEX     4
#define ESWGPIO_EXTI_IF        0x00000010UL

osThreadId_t buttonThreadID;
osThreadId_t buzzerThreadID;

int buzzerState = 0;
const uint32_t buttonExtIntThreadFlag = 0x00000001;
const uint32_t buzzerExtIntThreadFlag = 0x00000002;

void button_loop();
void buzzer_loop();
void initGPIOButton();
void buttonIntEnable();


/*
    Heartbeat Loop has following functionalities:
    - Initialization & Setup of GPIO pins; PA0 -> Buzzer Output & PA0 -> Button Input
    - Initialization of GPIO Button Interrupt
    - Creation of new thread for the button interrupt loop (Only triggers from the IRQ handler)
    - Enables button interrupt
*/
void hp_loop ()
{
    
    CMU_ClockEnable(cmuClock_GPIO, true);
    GPIO_PinModeSet(GPIO_BUZZER_PORT, GPIO_BUZZER_PIN, gpioModePushPull, buzzerState);
    GPIO_PinModeSet(GPIO_BUTTON_PORT, GPIO_BUTTON_PIN, gpioModeInputPullFilter, 1);

    initGPIOButton();

    const osThreadAttr_t buzzer_thread_attr = { .name = "Buzzer" };
    buzzerThreadID = osThreadNew(buzzer_loop, NULL, &buzzer_thread_attr);

    const osThreadAttr_t button_thread_attr = { .name = "Button" };
    buttonThreadID = osThreadNew(button_loop, NULL, &button_thread_attr);

    buttonIntEnable();
    
    for (;;);
}

/*
    Button Loop has following functionalities:
    - Waits until its flag is set from ISR
    - Conditionally toggles buzzer on and off 
        => If 1; will play, If 0; will not play. 
    - Conditionally toggles buzzer thread.
*/
void button_loop(void *args)
{
    for (;;)
    {
        osThreadFlagsClear(buttonExtIntThreadFlag);
        osThreadFlagsWait(buttonExtIntThreadFlag, osFlagsWaitAny, osWaitForever);

        if(buzzerState == 0) {
            buzzerState = 1;
            osThreadResume(buzzerThreadID);
        } else {
            buzzerState = 0;
            osThreadSuspend(buzzerThreadID);
        }   
    }
}

/*
    This function handles all logic that has to do 
    with generating buzzer tones. 
    
    It is OFF by default through buzzerState, plays when toggled by button.
*/
void buzzer_loop(void *args)
{
    for (;;)
    {
        if(buzzerState == 1) 
        {
            // 80bpm -> 3/4s -> 750ms
            int tempo = 750;
            int beats[] = {1, 1, 1, 1, 1, 1, 2};

            // Each note is represented by their estimated toggle periods
            int periods[] = {3, 3, 2, 2, 1, 1, 2}; 
            int periodsLength = sizeof(periods) / sizeof(int);

            /* For the length of the tune i.e periodsLength, 
                play one tone at a specified frequency -> 1000ms/period = XHz
            */
            for(int i=0; i<periodsLength; i++)
            {
                // Given toggle periods, how long tone should play
                int period = periods[i];
                int duration = (beats[i]*tempo)/period;

                for(int i=0; i<duration; i++)
                {
                    osDelay(period);
                    GPIO_PinOutToggle(GPIO_BUZZER_PORT, GPIO_BUZZER_PIN);
                }

                // Pause between each tone
                osDelay(5);
            }

            // Pause between each tune
            osDelay(100);
        } else;    
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

    info1("ESW-GPIO "VERSION_STR" (%d.%d.%d)", VERSION_MAJOR, VERSION_MINOR, VERSION_PATCH);

    // Initialize OS kernel
    osKernelInitialize();

    // Create task for heartbeat thread
    const osThreadAttr_t hp_thread_attr = { .name = "hp" };
    osThreadNew(hp_loop, NULL, &hp_thread_attr);

    if (osKernelReady == osKernelGetState())
    {
        // Switch to a task-safe logger
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

