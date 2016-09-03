using UnityEngine;
using System.Collections;
using System;

public class OptionsSave : SaveObject {

    private float controlSensitivity;

    public OptionsSave() {

    }

    public float GetControlSensitivity() {
        return this.controlSensitivity;
    }

    public void SetControlSensitivity(float sensitivity) {
        this.controlSensitivity = sensitivity;
    }
}
