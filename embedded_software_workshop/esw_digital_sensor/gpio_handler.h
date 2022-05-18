/**
 * @file gpio_handler.h
 *
 * @author Johannes Ehala, ProLab.
 * @license MIT
 *
 * Copyright ProLab, TTÜ. 2021
 */

#ifndef GPIO_HANDLER_H_
#define GPIO_HANDLER_H_

#include "cmsis_os2.h"
#include "i2c_handler.h" // Include I2C SDA and SCL port and pins

#define GPIO_EXTI_NUM           0
#define GPIO_IF_EXTI_NUM        0x00000001UL

#define MMA8653FC_SDA_PORT      gpioPortA
#define MMA8653FC_SCL_PORT      gpioPortA
#define MMA8653FC_INT_PORT      gpioPortA
#define MMA8653FC_SDA_PIN       3
#define MMA8653FC_SCL_PIN       2
#define MMA8653FC_INT_PIN       1

// Public functions
void gpio_i2c_pin_init (void);
void gpio_external_interrupt_init(void);
void gpio_external_interrupt_enable(osThreadId_t tID, uint32_t tFlag);
void gpio_external_interrupt_disable(void);

#endif // GPIO_HANDLER_H_
