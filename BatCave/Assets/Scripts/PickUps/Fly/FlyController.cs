using UnityEngine;
using System.Collections;

public class FlyController : MonoBehaviour {
    public Vector2 Speed = new Vector2(0.1f, 0.1f);
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(Speed.x, Speed.y);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (rb == null)
        {
            transform.Translate(Speed.x, Speed.y, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
        {
            EventManager.TriggerEvent(EventTypes.FLY_PICK_UP);
            Debug.Log("A fly has been picked up!");
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "CleanUp") {
            Destroy(gameObject);
        }
    }
}
