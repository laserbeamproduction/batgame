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
                Debug.Log("It's all so Purple here!");
                currentWallSprites = purpleCaveSprites;
                WallSpritesUpdated();
                break;
            case EnvironmentTypes.WOODS:
                Debug.Log("Run Forest Run!");
                currentWallSprites = woodsSprites;
                WallSpritesUpdated();
                break;
            default:
                Debug.Log("Environment not found!");
                break;
        }
    }

    private void WallSpritesUpdated() {
        Debug.Log("Wall sprites updated!");
        EventManager.TriggerEvent(EventTypes.WALL_SPRITES_UPDATED, currentWallSprites);
    }

}
