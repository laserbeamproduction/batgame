using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
    public GameObject Echo;
    public Transform PlayerPos;
    public int echoAmount = 0;

    private Vector2 movement;
    public float speed = 2;

    public float coolDownTime;
    private float currentCoolDownTime;
    private bool coolingDown;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        //movement = new Vector2(Input.GetAxis("Horizontal"), 0) * speed;
        movement = new Vector2(Input.acceleration.x, 0) * speed;

        if (Input.GetMouseButtonDown(0) && !coolingDown) {
            spawnEcho();
            currentCoolDownTime = coolDownTime;
        }

        checkCoolDown();
	}

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }

    void spawnEcho() {
        echoAmount += 1;
        Instantiate(Echo, new Vector3(PlayerPos.position.x, PlayerPos.position.y, -2), Quaternion.identity);
    }

    void checkCoolDown() {
        if (currentCoolDownTime > 0) {
            coolingDown = true;
            currentCoolDownTime -= Time.deltaTime;
        }

        if (currentCoolDownTime <= 0) {
            coolingDown = false;
        }
    }
}
