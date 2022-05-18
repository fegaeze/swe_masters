/**
 * @file mma8653fc_driver.h
 *
 * @author Johannes Ehala, ProLab.
 * @license MIT
 *
 * Copyright ProLab, TTÜ. 2021
 */

#ifndef MMA8653FC_DRIVER_H_
#define MMA8653FC_DRIVER_H_

typedef struct
{
    uint8_t status;     // Status registry value
    uint16_t out_x;     // Left-justified 2's complement format
    uint16_t out_y;     // Left-justified 2's complement format
    uint16_t out_z;     // Left-justified 2's complement format
} xyz_rawdata_t;

// Public functions
uint8_t read_whoami();
void sensor_reset (void);
void set_sensor_active ();
void set_sensor_standby ();
int8_t configure_xyz_data (uint8_t dataRate, uint8_t range, uint8_t powerMod);
int8_t configure_interrupt (uint8_t polarity, uint8_t pinmode, uint8_t interrupt, uint8_t int_select);

xyz_rawdata_t get_xyz_data();
int16_t convert_to_count(uint16_t raw_val);
float convert_to_g(uint16_t raw_val, uint8_t sensor_scale);

#endif // MMA8653FC_DRIVER_H_
