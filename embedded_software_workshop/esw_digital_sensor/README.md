# ESW-digital-sensor
Set up TTTW lab-kit accelerometer sensor (MMA8653FC) and measure acceleration. This includes:
 * GPIO pin setup for I2C and interrupts
 * I2C setup for sensor communication
 * Sensor configuring
 * Sensor data acquistion
 * Sensor data analysis

# Platforms
The application has been tested and should work with the following platforms:
 * Thinnect TestSystemBoard tsb0

# Build
 * Add project as submodule to the https://github.com/thinnect/node-apps.git project. Put it under 'node-apps/apps' directory. 
 * Open terminal and navigate to 'node-apps/apps/esw-gpio' directory and type 'make tsb0' to build project.
 * Standard build options apply, check the main [README](../../README.md).

# Flashing to uC
Accelerometer sensors are only available on the 2.1 and 2.2 microcontrollers of the TTTW labkit. 
 
# Resources
 * EFR32 Application Note on I2C
   https://www.silabs.com/documents/public/application-notes/AN0011.pdf
 * EFR32 Application Note on GPIO
   https://www.silabs.com/documents/public/application-notes/an0012-efm32-gpio.pdf
 * EFR32MG12 Wireless Gecko Reference Manual (GPIO p1105, I2C p501)
   https://www.silabs.com/documents/public/reference-manuals/efr32xg12-rm.pdf
 * GPIO API documentation 
   https://docs.silabs.com/mcu/latest/efr32mg12/group-GPIO
 * I2C API documentation 
   https://docs.silabs.com/mcu/latest/efr32mg12/group-I2C
 * ARM RTOS API
   https://arm-software.github.io/CMSIS_5/RTOS2/html/group__CMSIS__RTOS.html
 * MMA8653FC sensor datasheet
   https://www.nxp.com/docs/en/data-sheet/MMA8653FC.pdf
 * MMA8653FC sensor application note
   https://www.nxp.com/docs/en/application-note/AN4083.pdf
