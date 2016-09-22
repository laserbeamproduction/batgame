using UnityEngine;
using System.Collections;

public class WallSpriteSelector : MonoBehaviour {
    public Sprite[] currentWallSprites;
    public Sprite[] purpleCaveSprites;
    public Sprite[] woodsSprites;

	private void Start () {
        EventManager.StartListening(EventTypes.CHANGE_ENVIRONMENT, SetWallSprites);
	}

    /// <summary>
    /// Switch case to Set sprites and send a trigger to its manager
    /// </summary>
    /// <param name="environmentType">Object with proper environment name</param>
    private void SetWallSprites(object environmentType) {
        string environment = environmentType.ToString();

        switch (environment) {
            case EnvironmentTypes.PURPLE_CAVE:
                currentWallSprites = purpleCaveSprites;
                WallSpritesUpdated();
                break;
            case EnvironmentTypes.WOODS:
                currentWallSprites = woodsSprites;
                WallSpritesUpdated();
                break;
            default:
                Debug.Log("Environment not found!");
                break;
        }
    }

    private void WallSpritesUpdated() {
        EventManager.TriggerEvent(EventTypes.WALL_SPRITES_UPDATED);
        Debug.Log("Wall sprites updated!");
    }

}
