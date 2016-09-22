using UnityEngine;
using System.Collections;

public class BackgroundTileManager : MonoBehaviour {

    public SpriteRenderer[] bottomTiles, topTiles;

    public Sprite[] sprites;

    private float yBounds = -11f;
    private bool isTransition;

    // Use this for initialization
    void Start () {
        EventManager.StartListening(EventTypes.FLOOR_SPRITES_UPDATED, FloorSpritesUpdated);
        EventManager.StartListening(EventTypes.TRANSITION_START, TransitionStarted);
        EventManager.StartListening(EventTypes.TRANSITION_END, TransitionEnded);
        SetUpTiles();
    }
	
	void FixedUpdate () {
        if (!isTransition) {
            CheckTilePosition();
        }
    }

    private void TransitionStarted(object value)
    {
        isTransition = true;
    }

    private void TransitionEnded(object value)
    {
        isTransition = false;
    }

    //Get new sprites from WallSpriteSelector
    private void FloorSpritesUpdated(object value)
    {
        sprites = GetComponent<FloorSpriteSelector>().currentFloorSprites;
    }

    private void SetUpTiles() {
        foreach (SpriteRenderer tile in bottomTiles) {
            SetRandomSprite(tile);
        }
        foreach (SpriteRenderer tile in topTiles) {
            SetRandomSprite(tile);
        }
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
    }
}
