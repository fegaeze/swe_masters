/**
 * @file i2c_handler.c
 *
 * @brief Init I2C0 and transmit-receive.
 
 * @note The accelerometer sensor is always turned on on the TTTW lab-kit. So
 * no power ON or enable has to be done.
 * 
 * @author Johannes Ehala, ProLab.
 * @license MIT
 *
 * Copyright ProLab, TTÜ. 2021
 */

#include "em_cmu.h"

#include "i2c_handler.h"
#include "gpio_handler.h"

/**
 * @brief Init I2C interface. 
 *
 * Accelerometer sensor is connected to port A pin 2 (SCL) and pin 3 (SDA), I2C0
 * must be routed to those pins.
 */
void i2c_init (void)
{
    // Enable I2C clock.
    CMU_ClockEnable(cmuClock_I2C0, true);

    // Initialize and configure SDA and SCL pins for I2C data transfer.
    gpio_i2c_pin_init ();
	
	// Route I2C SDA and SCL to GPIO pins (efr32mg12-datasheet page 188).
	I2C0->ROUTELOC0 = (MMA8653FC_SCL_LOC | MMA8653FC_SDA_LOC); //0x00000083U
	I2C0->ROUTEPEN = 3;
    
    // Initialize I2C.
    I2C_Init_TypeDef i2c_init = I2C_INIT_DEFAULT;
    i2c_init.enable = false;
    I2C_Init(I2C0, &i2c_init);
}

void i2c_enable (void)
{
    I2C_Enable(I2C0, true);
}

void i2c_disable (void)
{
    I2C_Enable(I2C0, false);
}

void i2c_reset (void)
{
    I2C_Reset(I2C0);
}

I2C_TransferSeq_TypeDef * i2c_transaction (I2C_TransferSeq_TypeDef * seq)
{
    I2C_TransferReturn_TypeDef ret;
    
    // Do a polled transfer
    ret = I2C_TransferInit(I2C0, seq);
    while (ret == i2cTransferInProgress)
    {
      ret = I2C_Transfer(I2C0);
    }

    return seq;
}