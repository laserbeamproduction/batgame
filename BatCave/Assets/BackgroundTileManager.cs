using UnityEngine;
using System.Collections;

public class BackgroundTileManager : MonoBehaviour {

    public SpriteRenderer[] tiles;

    public Sprite[] sprites;

    private float resetYposition = 7f;

    // Use this for initialization
    void Start () {
        SetUpTiles();
    }
	
	// Update is called once per frame
	void Update () {
        CheckTilePosition();
    }

    private void SetUpTiles() {
        foreach (SpriteRenderer tile in tiles) {
            SetRandomSprite(tile);
        }
    }

    private void SetRandomSprite(SpriteRenderer tile) {
        tile.sprite = sprites[Mathf.RoundToInt(Random.Range(0, sprites.Length))];
    }

    /// <summary>
    /// Loop though the tile lists to check if they need repositioning
    /// </summary>
    private void CheckTilePosition() {
        foreach (SpriteRenderer tile in tiles) {

            // If a tile is not being rendered and its position is below the camera
            if (!tile.isVisible && tile.transform.position.y <= -7f) {
                ResetTile(tile);
            }
        }
    }

    /// <summary>
    /// Repositions the tile sprite back to the top and gives it a random sprite from the sprites array.
    /// </summary>
    /// <param name="tile"></param>
    private void ResetTile(SpriteRenderer tile) {
        tile.transform.position = new Vector3(tile.transform.position.x, resetYposition, tile.transform.position.z);
        SetRandomSprite(tile);
    }
}
