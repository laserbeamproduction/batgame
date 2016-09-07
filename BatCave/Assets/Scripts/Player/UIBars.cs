using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBars : MonoBehaviour {
    public PlayerControls player;
    public PlayerResources playerResources;

    public float staminaPercentage;
    public Slider staminaBar;
	
	// Update is called once per frame
	void Update () {
        UpdateResourceBar();
    }

    void UpdateResourceBar() {
        staminaPercentage = playerResources.stamina / playerResources.maxStamina;
        staminaBar.value = staminaPercentage;
    }

    public void ActivteShapeShift() {
        EventManager.TriggerEvent(EventTypes.SHAPE_SHIFT);
    }
}
