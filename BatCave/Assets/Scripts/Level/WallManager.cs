﻿using UnityEngine;
using UnityEngine.UI;

public class WallManager : MonoBehaviour {

    public SpriteRenderer[] leftWalls;
    public SpriteRenderer[] rightWalls;

    public Sprite[] sprites;

    private float resetYposition = 10.97f;

    // Use this for initialization
    void Start () {
        SetUpWalls();
    }
	
	// Update is called once per frame
	void Update () {
        CheckWallPosition();
    }

    /// <summary>
    /// Loop though the wall lists to check if they need repositioning
    /// </summary>
    private void CheckWallPosition() {
        foreach (SpriteRenderer wall in leftWalls) {

            // If a wall is not being rendered and its position is below the camera
            if (!wall.isVisible && wall.transform.position.y < -7f) {
                ResetWall(wall);
            }
        }

        foreach (SpriteRenderer wall in rightWalls) {

            // If a wall is not being rendered and its position is below the camera
            if (!wall.isVisible && wall.transform.position.y < -7f) {
                ResetWall(wall);
            }
        }
    }

    /// <summary>
    /// Repositions the wall sprite back to the top and gives it a random sprite from the sprites array.
    /// </summary>
    /// <param name="wall"></param>
    private void ResetWall(SpriteRenderer wall) {
        wall.transform.position = new Vector3(wall.transform.position.x, resetYposition, -1);
        SetRandomSprite(wall);
    }

    private void SetUpWalls() {

        float screenLeft = Screen.width - Screen.width;
        float screenRight = Screen.width;
        float screenVerticalMiddle = Screen.height / 2;

        foreach (SpriteRenderer wall in leftWalls) {
            PositionWallToCamera(wall, false);
            SetRandomSprite(wall);
        }

        foreach (SpriteRenderer wall in rightWalls) {
            PositionWallToCamera(wall, true);
            SetRandomSprite(wall);
        }
    }

    private void SetRandomSprite(SpriteRenderer wall) {
        wall.sprite = sprites[Mathf.RoundToInt(Random.Range(0, sprites.Length))];
    }

    private void PositionWallToCamera(SpriteRenderer wall, bool isRight) {
        float y = wall.transform.position.y;
        float x = isRight ? Screen.width : 0;
        Debug.Log(y);
        wall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, wall.transform.position.y, 0));
        wall.transform.position = new Vector3(wall.transform.position.x, y, -1);
    }
}
