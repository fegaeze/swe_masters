import chalk from "chalk";

export default class Carriageway {

  constructor(id, controller) {
    this.id = id;
    this.controller = controller;

    this.vehicleLights = null;
    this.pedestrianLights = null;

    this.callButtonReady = false;
    this.callButtonPressed = false;
    this.emergencyVehicleSignalReceived = false;
  }

  getCallButtonReady() {
    return this.callButtonReady;
  }

  setCallButtonReady(callButtonReady) {
    this.callButtonReady = callButtonReady;
  }

  getCallButtonPressed() {
    return this.callButtonPressed;
  }

  setCallButtonPressed(callButtonPressed) {
    this.callButtonPressed = callButtonPressed;
  }

  getEmergencyVehicleSignalReceived() {
    return this.emergencyVehicleSignalReceived;
  }

  setEmergencyVehicleSignalReceived(emergencyVehicleSignalReceived) {
    this.emergencyVehicleSignalReceived = emergencyVehicleSignalReceived;
  }

  onCallButtonPress() {
    this.setCallButtonPressed(true);
    if(this.callButtonReady) {
      this.controller.processCallButtonPress(this);
    }
    this.controller.updateDisplay();
  }

  onEmergencyVehicleSignalReceived() {
    this.setEmergencyVehicleSignalReceived(true);
    this.controller.updateDisplay();
  }

  setLights(vehicleLights, pedestrianLights) {
    this.vehicleLights = vehicleLights;
    this.pedestrianLights = pedestrianLights;
    this.controller.updateDisplay();
  }

  contentForDisplay() {
    const id = chalk.bold(this.id.toUpperCase());

    let vehicleLights = chalk.dim('OFF')
    if(this.vehicleLights) {
      let vehicleLightsText = this.vehicleLights.color.toUpperCase();
      if(this.vehicleLights.flash) vehicleLightsText += ` (FLASHING)`;
      vehicleLights = chalk[this.vehicleLights.color](vehicleLightsText);
    }

    let pedestrianLights = chalk.dim('OFF')
    if(this.pedestrianLights) {
      let pedestrianLightsText = this.pedestrianLights.color.toUpperCase();
      if(this.pedestrianLights.flash) pedestrianLightsText += ` (FLASHING)`;
      pedestrianLights = chalk[this.pedestrianLights.color](pedestrianLightsText);
    }

    const callButton = this.callButtonPressed ? chalk.whiteBright('PRESSED') : chalk.dim('NONE');
    const emergencyVehicleSignal = this.emergencyVehicleSignalReceived ? chalk.whiteBright('RECEIVED') : chalk.dim('NONE');

    const display = `
      ${id} Vehicles Lights:            ${vehicleLights}
      ${id} Pedestrians Lights:         ${pedestrianLights}
      ${id} Call Button:                ${callButton}
      ${id} Emergency Vehicle Signal:   ${emergencyVehicleSignal}
    `;

    return display;
  }
}
