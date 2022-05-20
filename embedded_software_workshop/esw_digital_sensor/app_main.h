/**
 * @file app_main.h
 * 
 * @author Johannes Ehala, ProLab.
 * @license MIT
 *
 * Copyright ProLab, TTÜ. 2021
 */

#ifndef ACCEL_APP_MAIN_H_
#define ACCEL_APP_MAIN_H_

#define SENSOR_DATA_RANGE     MMA8653FC_XYZ_DATA_CFG_2G_RANGE

#define INTERRUPT_POLARITY    MMA8653FC_CTRL_REG3_POLARITY_HIGH
#define INTERRUPT_PINMODE     MMA8653FC_CTRL_REG3_PINMODE_OD
#define INTERRUPT_DRDYEN      MMA8653FC_CTRL_REG4_DRDY_INT_EN
#define INTERRUPT_DRDYSEL     MMA8653FC_CTRL_REG5_DRDY_INTSEL_INT1

#endif // ACCEL_APP_MAIN_H_