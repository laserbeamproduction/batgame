using System;

[Serializable]
public class OptionsSave : SaveObject {

    private float controlSensitivity;

    public OptionsSave() {
        // default values
        // TODO: Move this to a config file
        this.controlSensitivity = 3f;
    }

    public float GetControlSensitivity() {
        return this.controlSensitivity;
    }

    public void SetControlSensitivity(float sensitivity) {
        this.controlSensitivity = sensitivity;
    }
}
