# Final Integration Project

The project uses a Raspberry Pi 4 running the Buster operating system. All of the software used were installed in a docker container using the [IOTstack](https://sensorsiot.github.io/IOTstack/) builder. They are Node-RED, Portainer, Mosquitto MQTT, and Grafana.       

For communication, I have set up three MQTT brokers. The first is Aedes, a node-red-based broker. This is for testing and debugging purposes only. The second broker is a containerized Mosquitto broker that interacts directly with the Node-RED installation. The third broker is HiveMQ, a cloud MQTT broker. It serves as a high-level overview of the system. The containerized Mosquitto broker also serves as a bridge between hiveMQ and the node-red installation. To configure the bridge, I modified the mosquitto.conf file in the mosquitto installation. The changes introduced are as follows:

```
connection hivemq_bridge
address broker.hivemq.com:1883

topic projekt-ulesanne__hazi-feladat_2022-spring-its8050/+/status in 0
topic projekt-ulesanne__hazi-feladat_2022-spring-its8050/+ out 0
topic projekt-ulesanne__hazi-feladat_2022-spring-its8050/sensor/temperature/data out 0
topic projekt-ulesanne__hazi-feladat_2022-spring-its8050/sensor/temperature in 0


bridge_protocol_version mqttv311
try_private false
notifications false
bridge_attempt_unsubscribe false
bridge_insecure true
```      

The project was built with resources provided in the class lectures and labs. Some of the notable ones are listed below: 
[RPi-RF](https://github.com/milaq/rpi-rf), [IOTstack](https://sensorsiot.github.io/IOTstack/), [Reference Document](https://docs.google.com/document/u/1/d/e/2PACX-1vT6YwZ4sUMra-_uoUigWn6EVMXz4cozK5bXy6bwp3It5dMzH-doEoh6LuGOPjuExDwWOIPVFEhD36WV/pub), [Steve's Node-RED Guide](https://stevesnoderedguide.com/), [Steve's MQTT Guide](http://www.steves-internet-guide.com/), [Official Node-RED Youtube Channel](https://www.youtube.com/c/NodeREDStudio), [Andreas Spiess Youtube Channel](https://www.youtube.com/c/AndreasSpiess)      
&nbsp;

### MQTT topics and subtopics
There are four main observables with which to interact, they define the topic hierarchy. The main topic is a long string that serves as a password. This is very useful when connecting to the public cloud mqtt broker. The next subtopic is determined by what needs to be interacted with. The "switch type" subtopic is for interactions with the switch. We have the 'status' subtopic for interacting with local MQTT broker, and I have chosen the "sensor" subtopic for interacting with CPU temperature data. This decision was taken with extensibility in mind. Other sorts of data can be added as children to the "sensor" subtopic if desired.

The topics and their heirarchy are as follows:
- The 433 switch toggle,
    - projekt-ulesanne__hazi-feladat_2022-spring-its8050/+/on - Toggle specified switch on (Pub)
    - projekt-ulesanne__hazi-feladat_2022-spring-its8050/+/off - Toggle specified switch off (Pub)
- Monitoring the local MQTT connection,
    - projekt-ulesanne__hazi-feladat_2022-spring-its8050/status - Display birth, death and last will message of mosquitto MQTT broker (Sub)
- The status of each switch
    - projekt-ulesanne__hazi-feladat_2022-spring-its8050/+/status - Get status for specified switch (Pub)
    - projekt-ulesanne__hazi-feladat_2022-spring-its8050/+ - Display status of specified switch (Sub)
- CPU temperature data
    - projekt-ulesanne__hazi-feladat_2022-spring-its8050/sensor/temperature - Get CPU tempertaure data (Pub)
    - projekt-ulesanne__hazi-feladat_2022-spring-its8050/sensor/temperature/data - Display CPU tempertaure data (Sub)      

I would like to note that the cloud broker does not have access to all topics. For example, it can only publish and subcribe to the switch and broker status topics. I have restricted it from being able to toggle the switches.

For the QOS, I have chosen to go with the option of "exactly once" across the topics except local broker status. I have gone with "at least once" for that.      
&nbsp;

### Installation Notes
- In the 433 GUI flow, the hostCmd node's command will have to be changed. The current command `ssh pi@192.168.0.100 /home/pi/work/rpi-rf/scripts/rpi-rf_send` is configured for my RPi and Node-RED installation.
- In the temperature sensor GUI flow, the hostTempCmd node's command will also have to be changed. The current command `ssh pi@192.168.0.100 vcgencmd measure_temp` is configured for my RPi and Node-RED installation.
- For the bridge to work, the above configuration will have to be added to your mosquitto.conf file.
&nbsp;
&nbsp;