import Carriageway from './carriageway.js';


export default class TrafficLightController {

  static startUpDelayThreshold = 10000;
  static startUpDelayDuration = 10000;
  static carriagewayWaitDuration = 8000;

  static carriagewayStates = {
    vehiclesPassingReady: {
      vehicleLights: {color: 'yellow', flash: false},
      pedestrianLights: {color: 'red', flash: false},
      duration: 5000,
      nextState: 'vehiclesPassing',
    },
    vehiclesPassing: {
      vehicleLights: {color: 'green', flash: false},
      pedestrianLights: {color: 'red', flash: false},
      duration: 10000,
      nextAction: 'vehiclePassingDurationComplete',
    },
    vehiclesPassingSlowDown: {
      vehicleLights: {color: 'green', flash: true},
      pedestrianLights: {color: 'red', flash: false},
      duration: 5000,
      nextState: 'vehiclesPassingAlmostDone',
    },
    vehiclesPassingAlmostDone: {
      vehicleLights: {color: 'yellow', flash: false},
      pedestrianLights: {color: 'red', flash: false},
      duration: 5000,
      nextState: 'vehiclesPassingDone',
    },
    vehiclesPassingDone: {
      vehicleLights: {color: 'red', flash: false},
      pedestrianLights: {color: 'red', flash: false},
      duration: 3000,
      nextState: 'pedestriansCrossing',
    },
    pedestriansCrossing: {
      vehicleLights: {color: 'red', flash: false},
      pedestrianLights: {color: 'green', flash: false},
      duration: 10000,
      nextState: 'pedestriansCrossingAlmostDone',
    },
    pedestriansCrossingAlmostDone: {
      vehicleLights: {color: 'red', flash: false},
      pedestrianLights: {color: 'green', flash: true},
      duration: 5000,
      nextState: 'pedestriansCrossingDone',
    },
    pedestriansCrossingDone: {
      vehicleLights: {color: 'red', flash: false},
      pedestrianLights: {color: 'red', flash: false},
      duration: 3000,
      nextState: 'vehiclesPassingReady',
    },
    emergencyVehicleApproaching: {
      vehicleLights: {color: 'green', flash: true},
      pedestrianLights: {color: 'red', flash: true},
      duration: 20000,
      nextState: 'vehiclesPassingSlowDown',
    }
  }

  constructor(displayInConsole = true) {
    this.c1 = new Carriageway('c1', this);
    this.c2 = new Carriageway('c2', this);
    this.displayInConsole = displayInConsole;
    this.downtime = null;
    this.starting = true;
    this.delayingStartUp = false;
    this.contentForDisplay = '';
  }

  start(downtime) {
    this.starting = true;
    this.downtime = downtime;
    this.delayStartUp = this.downtime && (this.downtime < TrafficLightController.startUpDelayThreshold);

    if(this.delayStartUp) {
      this.updateDisplay();

      setTimeout(() => {
        this.delayStartUp = false;
        this.startUpComplete();
      }, TrafficLightController.startUpDelayDuration);

    } else {
      this.startUpComplete()
    }
  }

  startUpComplete() {
    this.setCarriagewayState(this.c1, 'vehiclesPassingReady');
    this.setCarriagewayState(this.c2, 'vehiclesPassingReady');
  }

  setCarriagewayState(carriageway, state) {
    let { vehicleLights, pedestrianLights, duration, nextState, nextAction } = TrafficLightController.carriagewayStates[state];

    carriageway.setLights(vehicleLights, pedestrianLights);
    carriageway.setCallButtonReady(false);

    setTimeout(() => {
      let evp = carriageway.getEmergencyVehicleSignalReceived();

      if(evp && state !== 'pedestriansCrossing' && state != 'pedestriansCrossingAlmostDone') {
        carriageway.setEmergencyVehicleSignalReceived(false);
        nextState = 'emergencyVehicleApproaching';
        nextAction = null;
      }

      if(nextAction) {
        this[nextAction](carriageway, state);
      } else if(nextState) {
        this.setCarriagewayState(carriageway, nextState);
      }
    }, duration);
  }

  vehiclePassingDurationComplete(carriageway) {
    carriageway.setCallButtonReady(true);
    this.processCallButtonPress(carriageway);
  }

  processCallButtonPress(carriageway) {
    const callButtonPressed = carriageway.getCallButtonPressed();

    if(callButtonPressed) {
      carriageway.setCallButtonPressed(false);
      const otherCarriageway = carriageway === this.c1 ? this.c2 : this.c1;

      setTimeout(() => {
        this.setCarriagewayState(otherCarriageway, 'vehiclesPassingSlowDown');
      }, TrafficLightController.carriagewayWaitDuration);

      this.setCarriagewayState(carriageway, 'vehiclesPassingSlowDown');
    }
  }

  onCarriagewayInput(key) {
    if (key.name === '1') {
      this.c1.onCallButtonPress();
    } else if (key.name === '2') {
      this.c2.onCallButtonPress();
    } else if (key.name === '3') {
      this.c1.onEmergencyVehicleSignalReceived();
    } else if (key.name === '4') {
      this.c2.onEmergencyVehicleSignalReceived();
    }
  }

  updateDisplay() {
    this.contentForDisplay = `
      ========================================================================

      Dual-Carriageway Traffic Light Controller

      ========================================================================

      Inputs keys:

        '1' to press the Call Button press on Carriageway C1
        '2' to press the Call Button press on Carriageway C2
        '3' to send an Emergency Vehicle Signal to Carriageway C1
        '4' to send an Emergency Vehicle Signal to Carriageway C2
        'CTRL + C' to exit

      ------------------------------------------------------------------------
    `
    if(this.delayStartUp) {
      this.contentForDisplay += `
      System was down for only ${ this.downtime / 1000 } seconds.
      System will start in about ${ TrafficLightController.startUpDelayDuration / 1000 } seconds...

      ------------------------------------------------------------------------
      `
    }

    this.contentForDisplay += this.c1.contentForDisplay()
    this.contentForDisplay += this.c2.contentForDisplay()
    this.contentForDisplay += `
      ========================================================================
    `
    if(this.displayInConsole) {
      console.clear();
      console.log(this.contentForDisplay);
    }
  }

  getContentForDisplay(carriageway) {
    return carriageway.contentForDisplay();
  }
}
