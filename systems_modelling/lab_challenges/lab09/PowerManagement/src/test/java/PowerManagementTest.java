import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

// Implement a small test starting with an empty charge sending different actions
// (about 20 steps reasonably different steps) to it and assert that it never
// reaches more than 80% charged (never full).

// Assume that the battery moves from state to another every 30 minutes
// (either charging or discharging depending on state of the power switch).
class PowerManagementTests {

    @Test
    public void testSmall() {
        PowerManagement pm = new PowerManagement();
        assertEquals(pm.getBatteryController(), PowerManagement.BatteryController.Empty);
        assertEquals(pm.getSwitchController(), PowerManagement.SwitchController.On);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.setInactive();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.turnOff();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
        pm.forwardMinutes10();
        assertNotEquals(pm.getBatteryController(), PowerManagement.BatteryController.Full);
    }
}