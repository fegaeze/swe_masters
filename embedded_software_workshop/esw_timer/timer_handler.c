/**
 * @file timer_handler.c
 *
 * @brief Init TIMER0 to control buzzer with PWM signal.
 * 
 * @author Chioma Nkem-Eze
 * @license MIT
 *
 * Copyright ProLab, TTÜ. 15 January 2022
 */

#include "timer_handler.h"

#define ESWGPIO_LED0_PORT       gpioPortB
#define ESWGPIO_LED1_PORT       gpioPortB
#define ESWGPIO_LED2_PORT       gpioPortA

#define ESWGPIO_LED0_PIN        12
#define ESWGPIO_LED1_PIN        11
#define ESWGPIO_LED2_PIN        5 

/**
 * @brief Enable GPIO Peripheral clock, and initialize necessary ports and pins
 * for the LEDs and buttons. 
 */
void initialize_gpio_mod(void)
{
    CMU_ClockEnable(cmuClock_GPIO, true);

    GPIO_PinModeSet(ESWGPIO_LED0_PORT, ESWGPIO_LED0_PIN, gpioModePushPull, 0);
    GPIO_PinModeSet(ESWGPIO_LED1_PORT, ESWGPIO_LED1_PIN, gpioModePushPull, 0);
    GPIO_PinModeSet(ESWGPIO_LED2_PORT, ESWGPIO_LED2_PIN, gpioModePushPull, 0);
    GPIO_PinModeSet(GPIO_BUTTON_PORT, GPIO_BUTTON_PIN, gpioModeInputPullFilter, 1);
}


/**
 * @brief Init TIMER0 to regulate PWM dutycycle. 
 */
void initialize_timer_mod(void)
{
    // Enable clock.
    CMU_ClockEnable(cmuClock_TIMER0, true);

    // Timer Compare Mode Init and Config
    // Change mode and cofoa to relevant settinggs for PWM
    TIMER_InitCC_TypeDef timerCCInit = TIMER_INITCC_DEFAULT;
    timerCCInit.mode = timerCCModePWM;
    timerCCInit.cofoa = timerOutputActionToggle;

    // Initialize 3 timer channels to handle the LEDs
    TIMER_InitCC(TIMER0, 0, &timerCCInit);
    TIMER_InitCC(TIMER0, 1, &timerCCInit);
    TIMER_InitCC(TIMER0, 2, &timerCCInit);

    // Set Timer TOP value -> 100 in this case.
    TIMER_TopSet(TIMER0, TIMER_TOP);

    // Set default compare values for all three channels.
    // To achieve same brightness across the LEDs, give different figures for compare value
    TIMER_CompareSet(TIMER0, 0, TIMER_BLUE_DC); 
    TIMER_CompareSet(TIMER0, 1, TIMER_GREEN_DC);
    TIMER_CompareSet(TIMER0, 2, TIMER_RED_DC);  

    // Enable output connection to pin for the respective channels (1, 2, 3)
    // Set the location for the associated output pins in the repective channels
    // Blue  ->  B11 Channel 0, Location 5
    // Green ->  A5  Channel 1, Location 5  
    // Red   ->  B12 Channel 2, Location 5
    TIMER0->ROUTEPEN |= (TIMER_ROUTEPEN_CC0PEN | TIMER_ROUTEPEN_CC1PEN | TIMER_ROUTEPEN_CC2PEN);
    TIMER0->ROUTELOC0 |= (TIMER_ROUTELOC0_CC0LOC_LOC5 | TIMER_ROUTELOC0_CC1LOC_LOC5 | TIMER_ROUTELOC0_CC2LOC_LOC5);

    // Timer Peripheral Init and Config
    TIMER_Init_TypeDef timerInit = TIMER_INIT_DEFAULT;
    timerInit.prescale = timerPrescale256;
    timerInit.enable = true;
    
    TIMER_Init(TIMER0, &timerInit);
}

/**
 * @brief Generalized function to change duty cycle during software execution
 */
void duty_cycle(uint32_t blue, uint32_t green, uint32_t red)
{
    TIMER_CompareBufSet(TIMER0, 0, blue);
    TIMER_CompareBufSet(TIMER0, 1, green);
    TIMER_CompareBufSet(TIMER0, 2, red);
}
