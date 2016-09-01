using UnityEngine;
using System.Collections;

public class MoveObstacle : MonoBehaviour {
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
}
