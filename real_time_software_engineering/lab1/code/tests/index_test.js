import chalk from "chalk";
import assert from 'assert';

import TrafficLightController from '../src/traffic-light-controller.js'


const controller = new TrafficLightController(false);

const minimumWaitTime = 10000;
const codeLagAdjustment = 1000;
const startUpDelayDuration = 10000;
const carriagewayWaitDuration = 8000;
const vehiclesPassingReadyDuration = 5000;
const pedestriansCrossingDoneDuration = 3000;


describe('With Startup Sequence', function () {

  beforeEach(async function () {
    controller.startUpComplete();

    await new Promise(res => setTimeout(res, vehiclesPassingReadyDuration));
    controller.onCarriagewayInput({name: '1'});
  });

  describe('TC-001', function () {
    it('should transition to the “vehiclesPassingSlowDown” state on C1 after button press on C1', async function () { 
      await new Promise(res => setTimeout(res, minimumWaitTime));
      const display = await controller.getContentForDisplay(controller.c1);

      assert(display.includes(`${chalk.bold('C1')} Vehicles Lights:            ${chalk['green']('GREEN (FLASHING)')}`));
      assert(display.includes(`${chalk.bold('C1')} Pedestrians Lights:         ${chalk['red']('RED')}`));
    });
  });

  describe('TC-002', function () {
    it('should transition to the “vehiclesPassingSlowDown” state on C2 after button press on C1', async function () {
      const totalDuration = minimumWaitTime + carriagewayWaitDuration + codeLagAdjustment;

      await new Promise(res => setTimeout(res, totalDuration));
      const display = await controller.getContentForDisplay(controller.c2);

      assert(display.includes(`${chalk.bold('C2')} Vehicles Lights:            ${chalk['green']('GREEN (FLASHING)')}`));
      assert(display.includes(`${chalk.bold('C2')} Pedestrians Lights:         ${chalk['red']('RED')}`));
    });
  });

});


describe('Without startup sequence', function() {
  describe('TC-003', function () {

    before(async function () {
      controller.setCarriagewayState(controller.c1, "pedestriansCrossingDone");
    });
  
    it('should transition from the "pedestrianCrossingDone" state to the “vehiclesPassing” state in no more than 9 seconds', async function () {
      const totalDuration = pedestriansCrossingDoneDuration + vehiclesPassingReadyDuration + codeLagAdjustment;
  
      await new Promise(res => setTimeout(res, totalDuration));
      const display = await controller.getContentForDisplay(controller.c1);
  
      assert(display.includes(`${chalk.bold('C1')} Vehicles Lights:            ${chalk['green']('GREEN')}`));
      assert(display.includes(`${chalk.bold('C1')} Pedestrians Lights:         ${chalk['red']('RED')}`));
    });
  });
  
  describe('TC-004', function () {
    before(function () {
      controller.start(3);
    });

    it('should delay for 3 seconds before transitioning to the “vehiclesPassingReady” state', async function () {
      await new Promise(res => setTimeout(res, startUpDelayDuration));
      const display = await controller.getContentForDisplay(controller.c1);

      assert(display.includes(`${chalk.bold('C1')} Vehicles Lights:            ${chalk['yellow']('YELLOW')}`));
      assert(display.includes(`${chalk.bold('C1')} Pedestrians Lights:         ${chalk['red']('RED')}`));
    });
  });
})
