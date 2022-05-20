/**
 * @file mma8653fc_reg.h
 * 
 * @brieg Registry address map and bitfields for MMA8653FC sensor.
 * 
 * https://www.nxp.com/docs/en/data-sheet/MMA8653FC.pdf
 *
 * @author Johannes Ehala, ProLab.
 * @license MIT
 *
 * Copyright ProLab, TTÜ. 2021
 */

#ifndef MMA8653FC_REG_H_
#define MMA8653FC_REG_H_

/* MMA8653FC I2C slave address */

#define MMA8653FC_SLAVE_ADDRESS_READ        0x3B
#define MMA8653FC_SLAVE_ADDRESS_WRITE       0x3A

/* MMA8653FC registry address map */

#define MMA8653FC_REGADDR_STATUS            0x00
#define MMA8653FC_REGADDR_OUT_X_MSB         0x01
#define MMA8653FC_REGADDR_OUT_X_LSB         0x02
#define MMA8653FC_REGADDR_OUT_Y_MSB         0x03
#define MMA8653FC_REGADDR_OUT_Y_LSB         0x04
#define MMA8653FC_REGADDR_OUT_Z_MSB         0x05
#define MMA8653FC_REGADDR_OUT_Z_LSB         0x06
#define MMA8653FC_REGADDR_SYSMOD            0x0B
#define MMA8653FC_REGADDR_INT_SOURCE        0x0C
#define MMA8653FC_REGADDR_WHO_AM_I          0x0D
#define MMA8653FC_REGADDR_XYZ_DATA_CFG      0x0E
// TODO PL registries
// TODO FF registries
#define MMA8653FC_REGADDR_ASLP_COUNT        0x29
#define MMA8653FC_REGADDR_CTRL_REG1         0x2A
#define MMA8653FC_REGADDR_CTRL_REG2         0x2B
#define MMA8653FC_REGADDR_CTRL_REG3         0x2C
#define MMA8653FC_REGADDR_CTRL_REG4         0x2D
#define MMA8653FC_REGADDR_CTRL_REG5         0x2E
// TODO Offset registries

/* Bit fields for MMA8653FC STATUS registry */

#define MMA8653FC_STATUS_XDR_MASK           0x01
#define MMA8653FC_STATUS_YDR_MASK           0x02
#define MMA8653FC_STATUS_ZDR_MASK           0x04
#define MMA8653FC_STATUS_ZYXDR_MASK         0x08
#define MMA8653FC_STATUS_XOW_MASK           0x10
#define MMA8653FC_STATUS_YOW_MASK           0x20
#define MMA8653FC_STATUS_ZOW_MASK           0x40
#define MMA8653FC_STATUS_ZYXOW_MASK         0x80

/* Bit fields for MMA8653FC SYSMOD registry */

#define MMA8653FC_SYSMOD_MOD_STANDBY        0x00
#define MMA8653FC_SYSMOD_MOD_WAKE           0x01
#define MMA8653FC_SYSMOD_MOD_SLEEP          0x02
#define MMA8653FC_SYSMOD_MOD_MASK           0x03

/* Bit fields for MMA8653FC INT_SOURCE registry */

#define MMA8653FC_INT_SOURCE_DRDY_MASK      0x01
#define MMA8653FC_INT_SOURCE_FF_MT_MASK     0x04
#define MMA8653FC_INT_SOURCE_LNDPRT_MASK    0x10
#define MMA8653FC_INT_SOURCE_ASLP_MASK      0x80

/* Bit fields for MMA8653FC XYZ_DATA_CFG registry */

#define MMA8653FC_XYZ_DATA_CFG_2G_RANGE     0x00
#define MMA8653FC_XYZ_DATA_CFG_4G_RANGE     0x01
#define MMA8653FC_XYZ_DATA_CFG_8G_RANGE     0x02
#define MMA8653FC_XYZ_DATA_CFG_RANGE_MASK   0x03
#define MMA8653FC_XYZ_DATA_CFG_RANGE_SHIFT  0x00

/* TODO PL registries bit fields */
/* TODO FF_MT registries bit fields */
/* TODO ASLP registry bit fields */

/* Bit fields for MMA8653FC CTRL_REG1 registry */

#define MMA8653FC_CTRL_REG1_SAMODE_STANDBY  0x00
#define MMA8653FC_CTRL_REG1_SAMODE_ACTIVE   0x01
#define MMA8653FC_CTRL_REG1_SAMODE_MASK     0x01
#define MMA8653FC_CTRL_REG1_SAMODE_SHIFT    0x00

#define MMA8653FC_CTRL_REG1_FAST_READ       0x00
#define MMA8653FC_CTRL_REG1_NORMAL_READ     0x01
#define MMA8653FC_CTRL_REG1_READ_MOD_MASK   0x02
#define MMA8653FC_CTRL_REG1_READ_MOD_SHIFT  0x01

#define MMA8653FC_CTRL_REG1_DR_800HZ        0x00
#define MMA8653FC_CTRL_REG1_DR_400HZ        0x01
#define MMA8653FC_CTRL_REG1_DR_200HZ        0x02
#define MMA8653FC_CTRL_REG1_DR_100HZ        0x03
#define MMA8653FC_CTRL_REG1_DR_50HZ         0x04
#define MMA8653FC_CTRL_REG1_DR_12HZ         0x05 // 12.5 Hz
#define MMA8653FC_CTRL_REG1_DR_6HZ          0x06 // 6.25 Hz
#define MMA8653FC_CTRL_REG1_DR_1HZ          0x07 // 1.56 Hz
#define MMA8653FC_CTRL_REG1_DATA_RATE_MASK  0x38
#define MMA8653FC_CTRL_REG1_DATA_RATE_SHIFT 0x03

#define MMA8653FC_CTRL_REG1_ASLP_DR_50Hz    0x00
#define MMA8653FC_CTRL_REG1_ASLP_DR_12Hz    0x01 // 12.5 Hz
#define MMA8653FC_CTRL_REG1_ASLP_DR_6Hz     0x02 // 6.25 Hz
#define MMA8653FC_CTRL_REG1_ASLP_DR_1Hz     0x03 // 1.56 Hz
#define MMA8653FC_CTRL_REG1_ASPL_DR_MASK    0xC0
#define MMA8653FC_CTRL_REG1_ASPL_DR_SHIFT   0x06

/* Bit fields for MMA8653FC CTRL_REG2 registry */

#define MMA8653FC_CTRL_REG2_POWMOD_NORMAL   0x00 // Normal power mode
#define MMA8653FC_CTRL_REG2_POWMOD_LNLP     0x01 // Low noise, low power mode
#define MMA8653FC_CTRL_REG2_POWMOD_HIGHRES  0x02 // High resolution mode
#define MMA8653FC_CTRL_REG2_POWMOD_LOWPOW   0x03 // Low power mode

#define MMA8653FC_CTRL_REG2_ACTIVEPOW_MASK  0x03
#define MMA8653FC_CTRL_REG2_ACTIVEPOW_SHIFT 0x00

#define MMA8653FC_CTRL_REG2_ASLEEP_EN       0x01
#define MMA8653FC_CTRL_REG2_ASLEEP_MASK     0x04
#define MMA8653FC_CTRL_REG2_ASLEEP_SHIFT    0x02

#define MMA8653FC_CTRL_REG2_SLEEPPOW_MASK   0x18
#define MMA8653FC_CTRL_REG2_SLEEPPOW_SHIFT  0x03

#define MMA8653FC_CTRL_REG2_SOFTRST_EN      0x01
#define MMA8653FC_CTRL_REG2_SOFTRST_MASK    0x40
#define MMA8653FC_CTRL_REG2_SOFTRST_SHIFT   0x06

#define MMA8653FC_CTRL_REG2_SELFTEST_EN     0x01
#define MMA8653FC_CTRL_REG2_SELFTEST_MASK   0x80
#define MMA8653FC_CTRL_REG2_SELFTEST_SHIFT  0x07

/* Bit fields for MMA8653FC CTRL_REG3 registry */

#define MMA8653FC_CTRL_REG3_PINMODE_PP      0x00    // Pinmode Push-Pull
#define MMA8653FC_CTRL_REG3_PINMODE_OD      0x01    // Pinmode Open-Drain
#define MMA8653FC_CTRL_REG3_PINMODE_MASK    0x01
#define MMA8653FC_CTRL_REG3_PINMODE_SHIFT   0x00

#define MMA8653FC_CTRL_REG3_POLARITY_LOW    0x00    // Interrupt is signald by transition from high to low
#define MMA8653FC_CTRL_REG3_POLARITY_HIGH   0x01    // Interrupt is signald by transition from low to high
#define MMA8653FC_CTRL_REG3_POLARITY_MASK   0x02
#define MMA8653FC_CTRL_REG3_POLARITY_SHIFT  0x01

#define MMA8653FC_CTRL_REG3_WAKE_FFMT_EN    0x01
#define MMA8653FC_CTRL_REG3_WAKE_FFMT_MASK  0x08
#define MMA8653FC_CTRL_REG3_WAKE_FFMT_SHIFT 0x03

#define MMA8653FC_CTRL_REG3_WAKE_LP_EN      0x01
#define MMA8653FC_CTRL_REG3_WAKE_LP_MASK    0x20
#define MMA8653FC_CTRL_REG3_WAKE_LP_SHIFT   0x05

/* Bit fields for MMA8653FC CTRL_REG4 registry */

#define MMA8653FC_CTRL_REG4_DRDY_INT_EN     0x01
#define MMA8653FC_CTRL_REG4_DRDY_INT_MASK   0x01
#define MMA8653FC_CTRL_REG4_DRDY_INT_SHIFT  0x00

#define MMA8653FC_CTRL_REG4_FFMT_INT_EN     0x01
#define MMA8653FC_CTRL_REG4_FFMT_INT_MASK   0x04
#define MMA8653FC_CTRL_REG4_FFMT_INT_SHIFT  0x02

#define MMA8653FC_CTRL_REG4_LP_INT_EN       0x01
#define MMA8653FC_CTRL_REG4_LP_INT_MASK     0x10
#define MMA8653FC_CTRL_REG4_LP_INT_SHIFT    0x04

#define MMA8653FC_CTRL_REG4_ASLP_INT_EN     0x01
#define MMA8653FC_CTRL_REG4_ASLP_INT_MASK   0x80
#define MMA8653FC_CTRL_REG4_ASLP_INT_SHIFT  0x07

/* Bit fields for MMA8653FC CTRL_REG5 registry */

#define MMA8653FC_CTRL_REG5_DRDY_INTSEL_INT1    0x01
#define MMA8653FC_CTRL_REG5_DRDY_INTSEL_INT2    0x00
#define MMA8653FC_CTRL_REG5_DRDY_INTSEL_MASK    0x01
#define MMA8653FC_CTRL_REG5_DRDY_INTSEL_SHIFT   0x00

#define MMA8653FC_CTRL_REG5_FFMT_INTSEL_INT1    0x01
#define MMA8653FC_CTRL_REG5_FFMT_INTSEL_INT2    0x00
#define MMA8653FC_CTRL_REG5_FFMT_INTSEL_MASK    0x04
#define MMA8653FC_CTRL_REG5_FFMT_INTSEL_SHIFT   0x02

#define MMA8653FC_CTRL_REG5_LP_INTSEL_INT1      0x01
#define MMA8653FC_CTRL_REG5_LP_INTSEL_INT2      0x00
#define MMA8653FC_CTRL_REG5_LP_INTSEL_MASK      0x10
#define MMA8653FC_CTRL_REG5_LP_INTSEL_SHIFT     0x04

#define MMA8653FC_CTRL_REG5_ASLP_INTSEL_INT1    0x01
#define MMA8653FC_CTRL_REG5_ASLP_INTSEL_INT2    0x00
#define MMA8653FC_CTRL_REG5_ASLP_INTSEL_MASK    0x80
#define MMA8653FC_CTRL_REG5_ASLP_INTSEL_SHIFT   0x07

/* TODO Bit fields for MMA8653FC OFFSET registries */

#endif // MMA8653FC_REG_H_