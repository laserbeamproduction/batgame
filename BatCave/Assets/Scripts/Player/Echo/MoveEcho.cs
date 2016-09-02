using UnityEngine;
using System.Collections;

public class MoveEcho : MonoBehaviour {
    public float upTime;
    public Vector2 Speed = new Vector2(0.1f, 0.1f);
    private Rigidbody2D rb;
    public Light echo;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(Speed.x, Speed.y);
        }

        echo.intensity = 0;
        echo.spotAngle = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null)
        {
            transform.Translate(Speed.x, Speed.y, 0);
        }

        upTime -= Time.deltaTime;

        if (upTime < 0) {
            Destroy(gameObject);
        }

        if (echo.spotAngle < 120) {
           echo.spotAngle += 3;
        }

        if (echo.intensity < 8) {
            echo.intensity += 1;
        }

    }
}
