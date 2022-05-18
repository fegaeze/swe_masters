# GPIO example
A software project base to demonstrate GPIO usage example.
 * Use GPIO to toggel LEDs on/off. 
 * Use button and GPIO to generate software interrupts.

# Platforms
The application has been tested and should work with the following platforms:
 * Thinnect TestSystemBoard tsb0

# Build
 * Add project as submodule to the https://github.com/thinnect/node-apps.git project. Put it under 'node-apps/apps' directory. 
 * Open terminal and navigate to 'node-apps/apps/esw-gpio' directory and type 'make tsb0' to build project.

# Resources
 * EFR32 Application Note on GPIO
   https://www.silabs.com/documents/public/application-notes/an0012-efm32-gpio.pdf
 * EFR32MG12 Wireless Gecko Reference Manual (GPIO p1105)
   https://www.silabs.com/documents/public/reference-manuals/efr32xg12-rm.pdf
 * GPIO API documentation 
   https://docs.silabs.com/mcu/latest/efr32mg12/group-GPIO
 * ARM RTOS API
   https://arm-software.github.io/CMSIS_5/RTOS2/html/group__CMSIS__RTOS.html
