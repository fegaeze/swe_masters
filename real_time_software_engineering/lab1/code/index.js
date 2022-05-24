import readline from 'readline';

import TimeTracker from './src/time-tracker.js';
import TrafficLightController from './src/traffic-light-controller.js'


const controller = new TrafficLightController()
const timeTracker = new TimeTracker(controller);

// Handle keyboard events
readline.emitKeypressEvents(process.stdin);
process.stdin.setRawMode(true);

process.stdin.on('keypress', (str, key) => {
  if (key.ctrl && key.name === 'c') {
    process.exit();
  } else {
    controller.onCarriagewayInput(key)
  }
});

// start the controller

const downtime = timeTracker.getDowntime(controller);
controller.start(downtime);
