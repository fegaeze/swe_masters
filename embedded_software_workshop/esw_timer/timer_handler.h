/**
 * @file timer_handler.h
 *
 * @author Chioma Nkem-Eze
 * @license MIT
 *
 * Copyright ProLab, TTÜ. 15 January 2022
 */

#ifndef TIMER_HANDLER_H_
#define TIMER_HANDLER_H_

#include "em_cmu.h"
#include "em_timer.h"

#define TIMER_TOP               100

#define TIMER_RED_DC            25
#define TIMER_BLUE_DC           5
#define TIMER_GREEN_DC          100

#define GPIO_BUTTON_PIN         4
#define GPIO_BUTTON_PORT        gpioPortF

// Public functions
void initialize_gpio_mod(void);
void initialize_timer_mod(void);
void duty_cycle(uint32_t blue, uint32_t green, uint32_t red);

#endif // TIMER_HANDLER_H_ */
