using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBars : MonoBehaviour {
    public PlayerControls player;
    public PlayerResources playerResources;

    public float echoCooldownPercentage;
    public Slider echoCooldown;

    public float staminaPercentage;
    public Slider staminaBar;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        ResourceBar();
        EchoCooldown();
    }

    void EchoCooldown() {
        if (playerResources.stamina <= 0)
        {
            echoCooldown.value = 0;
        }
        else
        {
            echoCooldownPercentage = (player.coolDownTime - player.currentCoolDownTime) / player.coolDownTime;
            echoCooldown.value = echoCooldownPercentage;
        }
    }

    void ResourceBar() {
        staminaPercentage = playerResources.stamina / playerResources.maxStamina;
        staminaBar.value = staminaPercentage;
    }

    public void ActivteShapeShift() {
        EventManager.TriggerEvent(EventTypes.SHAPE_SHIFT);
    }
}
