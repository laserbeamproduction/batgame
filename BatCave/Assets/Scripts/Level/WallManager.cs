using UnityEngine;
using UnityEngine.UI;

public class WallManager : MonoBehaviour {
    public SpriteRenderer[] bottomWalls;
    public SpriteRenderer[] topWalls;

    //Current wall sprites
    public Sprite[] sprites;

    private float yBounds = -11f;
    private bool isTransition;

    void Start() {
        EventManager.StartListening(EventTypes.WALL_SPRITES_UPDATED, WallSpritesUpdated);
        EventManager.StartListening(EventTypes.TRANSITION_START, TransitionStarted);
        EventManager.StartListening(EventTypes.TRANSITION_END, TransitionEnded);
        SetupWalls();
    }

    private void SetupWalls()
    {
        /*foreach (SpriteRenderer wall in bottomWalls)
        {
            SetRandomSprite(wall);
        }
        foreach (SpriteRenderer wall in topWalls)
        {
            SetRandomSprite(wall);
        }*/
    }

    //Get new sprites from WallSpriteSelector
    private void WallSpritesUpdated(object newSprites) {
        sprites = newSprites as Sprite[];
    }

    private void TransitionStarted(object value) {
        isTransition = true;
    }

    private void TransitionEnded(object value) {
        isTransition = false;
    }

    void FixedUpdate () {
        //if (!isTransition) {
            CheckWallPosition();
        //}
    }

    /// <summary>
    /// Loop though the wall lists to check if they need repositioning
    /// </summary>
    private void CheckWallPosition() {
        foreach (SpriteRenderer wall in bottomWalls) {
            if (wall.transform.position.y <= yBounds) {
                ResetWall(wall, topWalls[0].transform.position.y + topWalls[0].bounds.size.y);
            }
        }

        foreach (SpriteRenderer wall in topWalls) {
            if (wall.transform.position.y <= yBounds) {
                ResetWall(wall, bottomWalls[0].transform.position.y + bottomWalls[0].bounds.size.y);
            }
        }
    }

    /// <summary>
    /// Repositions the wall sprite back to the top and gives it a random sprite from the sprites array.
    /// </summary>
    /// <param name="wall"></param>
    private void ResetWall(SpriteRenderer wall, float yPos) {
        wall.transform.position = new Vector3(wall.transform.position.x, yPos, wall.transform.position.z);
        SetRandomSprite(wall);
    }

    private void SetRandomSprite(SpriteRenderer wall) {
        wall.sprite = sprites[Mathf.RoundToInt(Random.Range(0, sprites.Length))];
    }
}
