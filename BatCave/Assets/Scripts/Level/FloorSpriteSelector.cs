using UnityEngine;
using System.Collections;

public class FloorSpriteSelector : MonoBehaviour {
    public Sprite[] purpleCaveSprites;
    public Sprite[] woodsSprites;

    private Sprite[] currentFloorSprites;

    private void Start()
    {
        EventManager.StartListening(EventTypes.CHANGE_ENVIRONMENT, SetFloorSprites);
    }

    private void SetFloorSprites(object environmentType)
    {
        string environment = environmentType.ToString();

        switch (environment)
        {
            case EnvironmentTypes.PURPLE_CAVE:
                currentFloorSprites = purpleCaveSprites;
                FloorSpritesUpdated();
                break;
            case EnvironmentTypes.WOODS:
                currentFloorSprites = woodsSprites;
                FloorSpritesUpdated();
                break;
            default:
                Debug.Log("Environment not found!");
                break;
        }
    }

    private void FloorSpritesUpdated()
    {
        EventManager.TriggerEvent(EventTypes.FLOOR_SPRITES_UPDATED, currentFloorSprites);
        Debug.Log("Floor sprites updated!");
    }
}
