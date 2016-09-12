using UnityEngine;
using UnityEngine.UI;

public class WallManager : MonoBehaviour {
    public SpriteRenderer[] leftWalls;
    public SpriteRenderer[] rightWalls;

    public Sprite[] sprites;

    private float resetYposition = 10.97f;
	
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
            if (wall.transform.position.y < -11f) {
                ResetWall(wall);
            }
        }

        foreach (SpriteRenderer wall in rightWalls) {

            // If a wall is not being rendered and its position is below the camera
            if (wall.transform.position.y < -11f) {
                ResetWall(wall);
            }
        }
    }

    /// <summary>
    /// Repositions the wall sprite back to the top and gives it a random sprite from the sprites array.
    /// </summary>
    /// <param name="wall"></param>
    private void ResetWall(SpriteRenderer wall) {
        wall.transform.position = new Vector2(wall.transform.position.x, resetYposition);
        SetRandomSprite(wall);
    }

    private void SetRandomSprite(SpriteRenderer wall) {
        wall.sprite = sprites[Mathf.RoundToInt(Random.Range(0, sprites.Length))];
    }
}
