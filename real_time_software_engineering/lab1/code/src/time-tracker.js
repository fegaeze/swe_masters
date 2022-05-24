
import Conf from 'conf';

export default class TimeTracker {

  constructor(controller) {
    this.controller = controller;
    this.dataStore = new Conf();
    this.downtime = null;

    this.calculateDowntime();
    this.trackTime();
  }

  getDowntime() {
    return this.downtime;
  }

  calculateDowntime() {
    const currentTimestamp = Date.now();
    const lastTimestamp = this.dataStore.get('lastTimestamp', null);
    this.downtime = currentTimestamp - lastTimestamp;
  }

  trackTime() {
    setInterval(() => {
      this.dataStore.set('lastTimestamp', Date.now());
    }, 1000);
  }
}