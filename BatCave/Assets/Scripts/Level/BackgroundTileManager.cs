using UnityEngine;
using System.Collections;

public class BackgroundTileManager : MonoBehaviour {
    public SpriteRenderer[] bottomTiles, topTiles;
    public GameObject transitionOut, transitionIn;

    public Sprite[] woodsSprites;

    private Sprite[] sprites;

    private float yBounds = -11f;
    public int treeTime;
    private int currentTreeTime;
    private bool isTransition = false;

    // Use this for initialization
    void Start () {
        EventManager.StartListening(EventTypes.TRANSITION_START, StartTransition);
        EventManager.StartListening(EventTypes.TRANSITION_END, EndTransition);
    }


    private void StartTransition(object value) {
        EnvironmentModel newSprites = value as EnvironmentModel;
        sprites = woodsSprites;
        isTransition = true;
    }

    private void EndTransition(object value) {
        EnvironmentModel newSprites = value as EnvironmentModel;
        sprites = newSprites.backgroundTiles as Sprite[];
        transitionOut.GetComponent<SpriteRenderer>().sprite = newSprites.transitionOut;
        transitionIn.GetComponent<SpriteRenderer>().sprite = newSprites.transitionIn;
    }

    void FixedUpdate () {
        CheckTilePosition();
    }

    private void SetRandomSprite(SpriteRenderer tile) {
        tile.sprite = sprites[Mathf.RoundToInt(Random.Range(0, sprites.Length))];
    }
    
    private void CheckTilePosition() {
        foreach (SpriteRenderer tile in bottomTiles) {
            if (tile.transform.position.y <= yBounds) {
                ResetTile(tile, topTiles[0].transform.position.y + topTiles[0].bounds.size.y);
            }
        }
        foreach (SpriteRenderer tile in topTiles) {
            if (tile.transform.position.y <= yBounds) {
                ResetTile(tile, bottomTiles[0].transform.position.y + bottomTiles[0].bounds.size.y);
            }
        }
    }
    /// <summary>
    /// Repositions the tile sprite back to the top and gives it a random sprite from the sprites array.
    /// </summary>
    /// <param name="tile"></param>
    private void ResetTile(SpriteRenderer tile, float yPos) {
            tile.transform.position = new Vector3(tile.transform.position.x, yPos, tile.transform.position.z);
            SetRandomSprite(tile);

        if (isTransition) {
            currentTreeTime++;
            if (currentTreeTime <= treeTime) {
                isTransition = false;
                currentTreeTime = 0;
                EventManager.TriggerEvent(EventTypes.CHANGE_ENVIRONMENT);
            }
        }
    }
}
