using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
    private float screenLeft;
    private float screenRight;
    private float screenHorizontalMiddle;

    public GameObject leftWall;
    public GameObject rightWall;

    new Camera camera;

    public float timer;
	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();

        //calulate where walls should be according to device width
        screenLeft = Screen.width - Screen.width;
        screenRight = Screen.width;
        screenHorizontalMiddle = Screen.height / 2;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
