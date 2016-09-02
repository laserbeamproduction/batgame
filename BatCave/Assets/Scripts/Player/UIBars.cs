using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBars : MonoBehaviour {
    public PlayerControls player;
    public float echoCooldownPercentage;
    public Slider echoCooldown;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        EchoCooldown();
    }

    void EchoCooldown() {
        echoCooldownPercentage = (player.coolDownTime - player.currentCoolDownTime) / player.coolDownTime;
        echoCooldown.value = echoCooldownPercentage;
    }
}
