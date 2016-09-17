using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBars : MonoBehaviour {
    public PlayerControls player;
    public PlayerResources playerResources;

    public float healthPercentage;
    public Slider healthBar;
	
	// Update is called once per frame
	void Update () {
        UpdateResourceBar();
    }

    void UpdateResourceBar() {
        healthPercentage = playerResources.health / playerResources.maxHealth;
        healthBar.value = healthPercentage;
    }

    public void ActivteShapeShift() {
        EventManager.TriggerEvent(EventTypes.SHAPE_SHIFT);
    }
}
