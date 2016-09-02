using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
    public GameObject Echo;
    public Transform PlayerPos;
    public int echoAmount = 0; //amount of times echo is used

    //movement
    private Vector2 movement;
    public float speed;

    //echo cooldown
    public float coolDownTime;
    public float currentCoolDownTime;
    private bool coolingDown;
    private Rigidbody2D rigidbody;
    public float timeBetweenEcho;
    public int echoSize = 7;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        speed = PlayerPrefs.GetFloat("motionSensitivity");
    }
	
	// Update is called once per frame
	void Update () {
        //movement = new Vector2(Input.GetAxis("Horizontal"), 0) * speed; //turn this on for desktop controls
        movement = new Vector2(Input.acceleration.x, 0) * speed; //turn this on for android controls

        if (Input.GetMouseButtonDown(0) && !coolingDown) {
            StartCoroutine(SpawnEcho());
            currentCoolDownTime = coolDownTime;
        }

        checkCoolDown();
	}

    void FixedUpdate()
    {
        rigidbody.velocity = movement;
    }

    IEnumerator SpawnEcho() {
        for (int i = 0; i < echoSize; i++) {
            Instantiate(Echo, new Vector3(PlayerPos.position.x, PlayerPos.position.y, -2), Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenEcho);
        }
        echoAmount += 1;
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
}
