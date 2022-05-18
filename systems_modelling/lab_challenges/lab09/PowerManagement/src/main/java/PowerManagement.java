public class PowerManagement {

    //------------------------
    // MEMBER VARIABLES
    //------------------------

    //PowerManagement Attributes
    private boolean isCharging;
    private boolean isActive;
    private boolean isSwitchedOn;
    private boolean isEmpty;
    private boolean isCharged25;
    private boolean isCharged40;
    private boolean isCharged60;
    private boolean isCharged80;
    private int tick = 0;

    //PowerManagement State Machines
    public enum BatteryController {Empty, Charged25, Charged40, Charged60, Charged80, Full, Charging, Discharging}

    private BatteryController batteryController;

    public enum SwitchController {On, Off}

    private SwitchController switchController;

    public enum Laptop {Active, Inactive}

    private Laptop laptop;

    //------------------------
    // CONSTRUCTOR
    //------------------------

    public PowerManagement() {
        isCharging = false;
        isActive = false;
        isSwitchedOn = true;
        isEmpty = true;
        isCharged25 = false;
        isCharged40 = false;
        isCharged60 = false;
        isCharged80 = false;
        setBatteryController(BatteryController.Empty);
        setSwitchController(SwitchController.On);
        setLaptop(Laptop.Active);
    }

    //------------------------
    // INTERFACE
    //------------------------

    public boolean setIsCharging(boolean aCharging) {
        boolean wasSet = false;
        isCharging = aCharging;
        wasSet = true;
        return wasSet;
    }

    public boolean setIsActive(boolean aIsActive) {
        boolean wasSet = false;
        isActive = aIsActive;
        wasSet = true;
        return wasSet;
    }

    public boolean setIsSwitchedOn(boolean aIsSwitchedOn) {
        boolean wasSet = false;
        isSwitchedOn = aIsSwitchedOn;
        wasSet = true;
        return wasSet;
    }

    public boolean setIsEmpty(boolean aIsEmpty) {
        boolean wasSet = false;
        isEmpty = aIsEmpty;
        wasSet = true;
        return wasSet;
    }

    public boolean setIsCharged25(boolean aIsCharged25) {
        boolean wasSet = false;
        isCharged25 = aIsCharged25;
        wasSet = true;
        return wasSet;
    }

    public boolean setIsCharged40(boolean aIsCharged40) {
        boolean wasSet = false;
        isCharged40 = aIsCharged40;
        wasSet = true;
        return wasSet;
    }

    public boolean setIsCharged60(boolean aIsCharged60) {
        boolean wasSet = false;
        isCharged60 = aIsCharged60;
        wasSet = true;
        return wasSet;
    }

    public boolean setIsCharged80(boolean aIsCharged80) {
        boolean wasSet = false;
        isCharged80 = aIsCharged80;
        wasSet = true;
        return wasSet;
    }

    public boolean getIsCharging() {
        return isCharging;
    }

    public boolean getIsActive() {
        return isActive;
    }

    public boolean getIsSwitchedOn() {
        return isSwitchedOn;
    }

    public boolean getIsEmpty() {
        return isEmpty;
    }

    public boolean getIsCharged25() {
        return isCharged25;
    }

    public boolean getIsCharged40() {
        return isCharged40;
    }

    public boolean getIsCharged60() {
        return isCharged60;
    }

    public boolean getIsCharged80() {
        return isCharged80;
    }

    public BatteryController getBatteryController() {
        return batteryController;
    }

    public SwitchController getSwitchController() {
        return switchController;
    }

    public Laptop getLaptop() {
        return laptop;
    }

    private boolean batteryStatusCheck() {
        boolean wasEventProcessed = false;

        BatteryController aBatteryController = batteryController;
        switch (aBatteryController) {
            case Empty:
                if (getIsCharging()) {
                    setBatteryController(BatteryController.Charged25);
                    setIsCharged25(true);
                    setIsCharging(true);
                    wasEventProcessed = true;
                } else {
                    setBatteryController(BatteryController.Charging);
                    setIsCharging(true);
                    wasEventProcessed = true;
                }
                break;
            case Charged25:
                if (getIsCharging()) {
                    setBatteryController(BatteryController.Charged40);
                    setIsCharged40(true);
                    setIsCharging(true);
                    wasEventProcessed = true;
                } else {
                    setBatteryController(BatteryController.Empty);
                    setIsCharging(false);
                    setIsEmpty(true);
                    wasEventProcessed = true;
                }
                break;
            case Charged40:
                if (getIsCharging() || getIsSwitchedOn()) {
                    setBatteryController(BatteryController.Charged60);
                    setIsCharged60(true);
                    setIsCharging(true);
                    wasEventProcessed = true;
                } else {
                    setBatteryController(BatteryController.Charged25);
                    setIsCharged25(true);
                    wasEventProcessed = true;
                }
                break;
            case Charged60:
                if (getIsCharging()) {
                    setBatteryController(BatteryController.Charged80);
                    setIsCharged80(true);
                    setIsCharging(true);
                    wasEventProcessed = true;
                } else {
                    setBatteryController(BatteryController.Charged40);
                    setIsCharged40(true);
                    wasEventProcessed = true;
                }
                break;
            case Charged80:
                if (getIsSwitchedOn()) {
                    setBatteryController(BatteryController.Discharging);
                    setIsCharged80(true);
                    setIsCharging(false);
                    wasEventProcessed = true;
                    break;
                }
                break;
            case Charging:
                if (getIsCharged80()) {
                    setBatteryController(BatteryController.Charged80);
                    setIsCharged80(true);
                    setIsCharging(false);
                    wasEventProcessed = true;
                    break;
                }
                if (getIsEmpty() && getIsSwitchedOn()) {
                    setBatteryController(BatteryController.Charged25);
                    setIsCharged25(true);
                    setIsCharging(true);
                    wasEventProcessed = true;
                    break;
                }
                if (getIsCharged25() && getIsSwitchedOn()) {
                    setBatteryController(BatteryController.Charged40);
                    setIsCharged40(true);
                    setIsCharging(true);
                    wasEventProcessed = true;
                    break;
                }
                if (getIsCharged40() && getIsSwitchedOn()) {
                    setBatteryController(BatteryController.Charged60);
                    setIsCharged60(true);
                    setIsCharging(true);
                    wasEventProcessed = true;
                    break;
                }
                if (getIsCharged60() && getIsSwitchedOn()) {
                    setBatteryController(BatteryController.Charged80);
                    setIsCharged80(true);
                    setIsCharging(true);
                    wasEventProcessed = true;
                    break;
                }
                break;
            case Discharging:
                if (getIsCharged80() && !getIsCharging()) {
                    setBatteryController(BatteryController.Charged60);
                    setIsCharged60(true);
                    setIsCharging(false);
                    wasEventProcessed = true;
                    break;
                }
                if (getIsCharged60() && !getIsCharging()) {
                    setBatteryController(BatteryController.Charged40);
                    setIsCharged40(true);
                    setIsCharging(false);
                    wasEventProcessed = true;
                    break;
                }
                if (getIsCharged40() && getIsSwitchedOn()) {
                    setBatteryController(BatteryController.Charging);
                    setIsCharging(true);
                    setIsCharged40(true);
                    wasEventProcessed = true;
                    break;
                }
                if (getIsCharged40() && getIsSwitchedOff()) {
                    setBatteryController(BatteryController.Charged25);
                    setIsCharging(false);
                    setIsCharged25(true);
                    wasEventProcessed = true;
                    break;
                }
                if (getIsCharged25() && getIsSwitchedOff()) {
                    setBatteryController(BatteryController.Empty);
                    setIsEmpty(true);
                    setIsCharging(false);
                    wasEventProcessed = true;
                    break;
                }
                break;
            default:
                // Other states do respond to this event
        }

        return wasEventProcessed;
    }

    private boolean getIsSwitchedOff() {
        return !this.isSwitchedOn;
    }

    public boolean consumesBatteryPower() {
        boolean wasEventProcessed = false;

        BatteryController aBatteryController = batteryController;
        switch (aBatteryController) {
            case Charged80:
                setBatteryController(BatteryController.Discharging);
                wasEventProcessed = true;
                break;
            case Full:
                setBatteryController(BatteryController.Discharging);
                wasEventProcessed = true;
                break;
            default:
                // Other states do respond to this event
        }

        return wasEventProcessed;
    }

    public boolean turnOff() {
        boolean wasEventProcessed = false;

        SwitchController aSwitchController = switchController;
        switch (aSwitchController) {
            case On:
                setSwitchController(SwitchController.Off);
                wasEventProcessed = true;
                break;
            default:
                // Other states do respond to this event
        }

        return wasEventProcessed;
    }

    public boolean turnOn() {
        boolean wasEventProcessed = false;

        SwitchController aSwitchController = switchController;
        switch (aSwitchController) {
            case Off:
                setSwitchController(SwitchController.On);
                wasEventProcessed = true;
                break;
            default:
                // Other states do respond to this event
        }

        return wasEventProcessed;
    }

    public boolean setInactive() {
        boolean wasEventProcessed = false;

        Laptop aLaptop = laptop;
        switch (aLaptop) {
            case Active:
                setLaptop(Laptop.Inactive);
                wasEventProcessed = true;
                break;
            default:
                // Other states do respond to this event
        }

        return wasEventProcessed;
    }

    public boolean setActive() {
        boolean wasEventProcessed = false;

        Laptop aLaptop = laptop;
        switch (aLaptop) {
            case Inactive:
                setLaptop(Laptop.Active);
                wasEventProcessed = true;
                break;
            default:
                // Other states do respond to this event
        }

        return wasEventProcessed;
    }

    private void setBatteryController(BatteryController aBatteryController) {
        batteryController = aBatteryController;
    }

    private void setSwitchController(SwitchController aSwitchController) {
        switchController = aSwitchController;

        // entry actions and do activities
        switch (switchController) {
            case On:
                // line 44 "model.ump"
                turnOn();
                break;
        }
    }

    private void setLaptop(Laptop aLaptop) {
        laptop = aLaptop;

        // entry actions and do activities
        switch (laptop) {
            case Active:
                // line 54 "model.ump"
                setActive();
                break;
            case Inactive:
                // line 58 "model.ump"
                setInactive();
                break;
        }
    }

    public void delete() {
    }

    public String toString() {
        return super.toString() + "[" +
                "charging" + ":" + getIsCharging() + "," +
                "isActive" + ":" + getIsActive() + "," +
                "isSwitchedOn" + ":" + getIsSwitchedOn() + "," +
                "isCharged40" + ":" + getIsCharged40() + "," +
                "isCharged60" + ":" + getIsCharged60() + "," +
                "isCharged80" + ":" + getIsCharged80() + "]";
    }

    public void forwardMinutes10() {
        System.out.println("\n⌛ 10 min passed");
        this.tick += 1;
        batteryStatusCheck();
        report();
    }

    public void report() {
        System.out.printf("Switch controller: %s\nBattery controller: %s\nLaptop: %s\nIs charging: %s\n",
                this.switchController.toString(),
                this.batteryController.toString(),
                this.laptop.toString(),
                this.getIsCharging());
    }
}