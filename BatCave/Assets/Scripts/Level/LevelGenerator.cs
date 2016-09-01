using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
    private float screenLeft;
    private float screenRight;
    private float screenHorizontalMiddle;

    public GameObject leftWall;
    public GameObject rightWall;

    new Camera camera;
	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        screenLeft = Screen.width - Screen.width;
        screenRight = Screen.width;
        screenHorizontalMiddle = Screen.height / 2;

        setUpWalls();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void setUpWalls()
    {
        leftWall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenLeft, screenHorizontalMiddle, 1));
        rightWall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenRight, screenHorizontalMiddle, 1));
    }
}
