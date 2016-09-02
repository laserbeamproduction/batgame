using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public Slider movementSensitivitySlider;

	// Use this for initialization
	void Start () {
        movementSensitivitySlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
        movementSensitivitySlider.value = PlayerPrefs.GetFloat("motionSensitivity") / 10;
    }
	
	// Update is called once per frame
	void Update () {

        // Android Backbutton is down
        if (Input.GetKeyDown(KeyCode.Escape)) {
            LoadingController.LoadScene(LoadingController.Scenes.MAIN_MENU);
        }
	}

    public void OnValueChanged() {
        float value = movementSensitivitySlider.value * 10;
        Debug.Log(value);
        PlayerPrefs.SetFloat("motionSensitivity", value);
    }

}

