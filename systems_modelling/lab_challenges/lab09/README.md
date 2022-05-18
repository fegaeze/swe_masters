# Lab 09

## Power management statechart

The state chart was done using Umple. The code has been defined below. Ihar and I (Chioma) had a session where we went over the requirements and came out with the possible states and transitions. Also attached below is the generated state chart from Umple:

```java
class PowerManagement {
    Boolean charging = false;
    Boolean isActive = false;
    Boolean isSwitchedOn = false;
    Boolean isCharged40 = false;
    Boolean isCharged60 = false;
    Boolean isCharged80 = false;

    BatteryController {
        Empty {
            batteryStatusCheck [getIsSwitchedOn()] -> Charging;
        }
        Charged25 {
            batteryStatusCheck [getIsSwitchedOff()] -> Empty;
            batteryStatusCheck [getIsSwitchedOn()] -> Charging;
        }
        Charged40 {
            batteryStatusCheck [getIsSwitchedOn()] -> Charging;
        }
        Charged60 {
            batteryStatusCheck [getIsSwitchedOn()] -> Charging;
        }
        Charged80 {
            consumesBatteryPower -> Discharging;
            batteryStatusCheck [getIsSwitchedOff()] -> Full;
        }
        Full {
            consumesBatteryPower -> Discharging;
        }
        Charging {
            batteryStatusCheck [getIsCharged80()] -> Charged80;
        }
        Discharging {
            batteryStatusCheck [getIsSwitchedOff()] -> Charged25;
            batteryStatusCheck [getIsCharged40()] -> Charged40;
            batteryStatusCheck [getIsCharged60()] -> Charged60;
        }
    }

    SwitchController {
        On {
            entry/ {turnOn();}
            turnOff -> Off;
        }
        Off {
            turnOn -> On;
        }
    }

    Laptop {
        Active {
            entry/ {setActive();}
            setInactive -> Inactive;
        }
        Inactive {
            entry/ {setInactive();}
            setActive -> Active;
        }
    }

    void turnOn() {
        System.out.println("turned on");
        this.isSWitchOn = true;
    }

    void setActive() {
        System.out.println("set active");
        this.isActive = true;
    }

    void setInactive() {
        System.out.println("set inactive");
        this.isActive = false;
    }
}
```

<img src="../../portfolios/jessica/assets/lab09/statechart_01.png" alt="State Chart - Battery Controller" width="500"/>
<img src="../../portfolios/jessica/assets/lab09/statechart_02.png" alt="State Chart - Switch Controller" width="500"/>

## Power management test

We generated the power management skeleton code from Umple. It still required some refactoring and simplification. The code is located at the [PowerManagement](PowerManagement) subdirectory. Here's the output of the small test:

```
⌛ 10 min passed
Switch controller: On
Battery controller: Charging
Laptop: Active
Is charging: true

⌛ 10 min passed
Switch controller: On
Battery controller: Charged25
Laptop: Active
Is charging: true

⌛ 10 min passed
Switch controller: On
Battery controller: Charged40
Laptop: Active
Is charging: true

⌛ 10 min passed
Switch controller: On
Battery controller: Charged60
Laptop: Active
Is charging: true

⌛ 10 min passed
Switch controller: On
Battery controller: Charged80
Laptop: Active
Is charging: true

⌛ 10 min passed
Switch controller: On
Battery controller: Discharging
Laptop: Active
Is charging: false

⌛ 10 min passed
Switch controller: On
Battery controller: Charged60
Laptop: Active
Is charging: false

⌛ 10 min passed
Switch controller: On
Battery controller: Charged40
Laptop: Active
Is charging: false

⌛ 10 min passed
Switch controller: On
Battery controller: Charged60
Laptop: Active
Is charging: true

⌛ 10 min passed
Switch controller: On
Battery controller: Charged80
Laptop: Active
Is charging: true

⌛ 10 min passed
Switch controller: On
Battery controller: Discharging
Laptop: Active
Is charging: false

⌛ 10 min passed
Switch controller: On
Battery controller: Charged60
Laptop: Inactive
Is charging: false

⌛ 10 min passed
Switch controller: On
Battery controller: Charged40
Laptop: Inactive
Is charging: false

⌛ 10 min passed
Switch controller: On
Battery controller: Charged60
Laptop: Inactive
Is charging: true

⌛ 10 min passed
Switch controller: Off
Battery controller: Charged80
Laptop: Inactive
Is charging: true

⌛ 10 min passed
Switch controller: Off
Battery controller: Discharging
Laptop: Inactive
Is charging: false

⌛ 10 min passed
Switch controller: Off
Battery controller: Charged60
Laptop: Inactive
Is charging: false

⌛ 10 min passed
Switch controller: Off
Battery controller: Charged40
Laptop: Inactive
Is charging: false

⌛ 10 min passed
Switch controller: Off
Battery controller: Charged60
Laptop: Inactive
Is charging: true

⌛ 10 min passed
Switch controller: Off
Battery controller: Charged80
Laptop: Inactive
Is charging: true
```

We see that after 20 steps (200 min) and different actions, the laptop was never fully charged or fully dischaged (except only in the beginning) while the switch controller was on.