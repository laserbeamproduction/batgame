using UnityEngine;

public class SimpleMoveScript : MonoBehaviour {

    public float speed;
    public Vector3 direction;

    void Start() {
        this.speed = SaveLoadController.GetInstance().GetPlayer().GetSpeed();
        EventManager.StartListening(EventTypes.PLAYER_SPEED_CHANGED, OnSpeedChanged);
    }

    // Update is called once per frame
    void FixedUpdate() {
        this.gameObject.transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnSpeedChanged() {
        this.speed = SaveLoadController.GetInstance().GetPlayer().GetSpeed();
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.PLAYER_SPEED_CHANGED, OnSpeedChanged);
    }
}
