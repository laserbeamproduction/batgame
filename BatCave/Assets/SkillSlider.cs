using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillSlider : MonoBehaviour {

    public float speed;
    public float coolDown;

    private Slider slider;
    private bool isMovingLeft;
    private float coolDownTimer;
    private bool onCooldown;

    private float lastSkillValue;

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
        StartAtRandomPosition();
        coolDownTimer = 0f;

        // Add event listener for when the player uses beam
        EventManager.StartListening(EventTypes.ECHO_USED, OnSkillShotTriggered);
    }

    // DEBUG CODE
    void Update() {
        if (Input.GetMouseButtonDown(0))
            OnSkillShotTriggered();
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.ECHO_USED, OnSkillShotTriggered);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (onCooldown)
            UpdateCoolDown();

        if (coolDownTimer <= 0f)
            Move();
        else 
            coolDownTimer -= Time.deltaTime;
	}

    void StartAtRandomPosition() {
        slider.value = Random.Range(slider.minValue, slider.maxValue);
        isMovingLeft = Random.value > 0.5f;
    }

    void UpdateCoolDown() {
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer <= 0f) {
            onCooldown = false;
            coolDownTimer = coolDown;
        }
    }

    void Move() {
        float direction = isMovingLeft ? -1 : 1;
        if (isMovingLeft) {
            isMovingLeft = slider.value > slider.minValue;
            Debug.Log(slider.value + "    " + slider.minValue);
        } else {
            isMovingLeft = slider.value >= slider.maxValue;
        }
        slider.value += (direction * speed * Time.deltaTime);
    }

    void OnSkillShotTriggered() {
        if (onCooldown)
            return;

        // set value
        lastSkillValue = slider.value;

        // dispatch value for the beam
        EventManager.TriggerEvent(EventTypes.SKILL_VALUE);

        // Activate cooldown
        onCooldown = true;
    }

    public float GetLastSkillValue() {
        return lastSkillValue;
    }
}
