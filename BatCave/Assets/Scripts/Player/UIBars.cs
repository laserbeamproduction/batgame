using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBars : MonoBehaviour {
    public PlayerControls player;
    public PlayerResources playerResources;

    public float staminaPercentage;
    public Slider staminaBarRight;
    public Slider staminaBarLeft;
	
	// Update is called once per frame
	void Update () {
        UpdateResourceBar();
    }

    void UpdateResourceBar() {
        staminaPercentage = playerResources.stamina / playerResources.maxStamina;
        staminaBarRight.value = staminaPercentage;
        staminaBarLeft.value = staminaPercentage;
    }

    public void ActivteShapeShift() {
        EventManager.TriggerEvent(EventTypes.SHAPE_SHIFT);
    }
}
