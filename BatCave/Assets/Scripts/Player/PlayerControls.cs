using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
    public GameObject Echo;
    public Transform PlayerPos;
    public int echoAmount = 0; //amount of times echo is used

    //movement
    private Vector2 movement;
    public float speed;

    //Collisions
    public ScoreCalculator score;

    //echo
    public float coolDownTime;
    public float currentCoolDownTime;
    private bool coolingDown;
    private Rigidbody2D rigidbody;
    public PlayerResources playerResources;

    //Shape Shift
    public bool isShapeShifted;
    public Light playerLight;
    public float batLightRange;
    public float draculaLightRange;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        speed = SaveLoadController.GetInstance().GetOptions().GetControlSensitivity();
    }
	
	// Update is called once per frame
	void Update () {
        //movement = new Vector2(Input.GetAxis("Horizontal"), 0) * speed; //turn this on for desktop controls
        movement = new Vector2(Input.acceleration.x, 0) * speed; //turn this on for android controls
        checkCoolDown();
    }

    void FixedUpdate()
    {
        rigidbody.velocity = movement;
    }

    public void SpawnEcho() {
        if (!coolingDown && playerResources.stamina > 0)
        {
            currentCoolDownTime = coolDownTime;
            EventManager.TriggerEvent(EventTypes.ECHO_USED);
            echoAmount += 1;
            Instantiate(Echo, new Vector3(PlayerPos.position.x, PlayerPos.position.y, -2), Quaternion.identity);
        }
    }

    void checkCoolDown() {
        if (currentCoolDownTime > 0) {
            coolingDown = true;
            currentCoolDownTime -= Time.deltaTime;
        }

        if (currentCoolDownTime <= 0) {
            currentCoolDownTime = 0;
            coolingDown = false;
        }
    }

    public void ShapeShift() {
        if (!isShapeShifted) {
            isShapeShifted = true;
            playerLight.spotAngle = draculaLightRange;
            Debug.Log("ShapeShift");
        }

        else if (isShapeShifted) {
            isShapeShifted = false;
            playerLight.spotAngle = batLightRange;
            Debug.Log("Not ShapeShifted");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle") //simple check if player is colliding with obstacles
        {
            EventManager.TriggerEvent(EventTypes.GAME_OVER);
        }
    }
}
