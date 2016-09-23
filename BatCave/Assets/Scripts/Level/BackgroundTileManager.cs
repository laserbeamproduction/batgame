using UnityEngine;
using System.Collections;

public class BackgroundTileManager : MonoBehaviour {

    public SpriteRenderer[] bottomTiles, topTiles;

    public Sprite[] sprites;
    public Sprite[] transitionSprites;

    private float yBounds = -11f;

    private bool isTransitionOut;
    private bool isTransitionIn;

    // Use this for initialization
    void Start () {
        EventManager.StartListening(EventTypes.FLOOR_SPRITES_UPDATED, FloorSpritesUpdated);
        EventManager.StartListening(EventTypes.SET_TRANSITION_OUT_SPRITES, SetTransitionOutSprites);
        SetUpTiles();
    }
	
	void FixedUpdate () {
        CheckTilePosition();
    }

    private void SetTransitionOutSprites(object transitionOutSprites) {
        transitionSprites = transitionOutSprites as Sprite[];
        isTransitionOut = true;
    }

    //Get new sprites from WallSpriteSelector
    private void FloorSpritesUpdated(object newSprites)
    {
        sprites = newSprites as Sprite[];
    }

    private void SetUpTiles() {
        /*foreach (SpriteRenderer tile in bottomTiles) {
            SetRandomSprite(tile);
        }
        foreach (SpriteRenderer tile in topTiles) {
            SetRandomSprite(tile);
        }*/
    }

    private void SetTransitionSprite(SpriteRenderer tile) {
        tile.sprite = transitionSprites[Mathf.RoundToInt(Random.Range(0, sprites.Length))];
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
        if (isTransitionOut)
        {
            tile.transform.position = new Vector3(tile.transform.position.x, yPos, tile.transform.position.z);
            SetTransitionSprite(tile);
            isTransitionOut = false;
            EventManager.TriggerEvent(EventTypes.TRANSITION_ACTIVE);
        }
        else if (isTransitionIn)
        {
            tile.transform.position = new Vector3(tile.transform.position.x, yPos, tile.transform.position.z);
            SetTransitionSprite(tile);
            isTransitionIn = false;
            EventManager.TriggerEvent(EventTypes.TRANSITION_END);
        }
        else {
            tile.transform.position = new Vector3(tile.transform.position.x, yPos, tile.transform.position.z);
            SetRandomSprite(tile);
        }
    }
}
