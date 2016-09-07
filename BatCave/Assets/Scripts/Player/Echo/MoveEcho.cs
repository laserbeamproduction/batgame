using UnityEngine;
using System.Collections;

public class MoveEcho : MonoBehaviour {
    public float upTime;
    public Vector2 Speed = new Vector2(0.1f, 0.1f);
    private Rigidbody2D rb;
    public Light echo;

    private float maxSpotAngle;
    public float defaultSpotAngle = 120;
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

        if (echo.spotAngle < maxSpotAngle) {
           echo.spotAngle += 3;
        }

        if (echo.intensity < 8) {
            echo.intensity += 1;
        }
    }

    public void EchoSize(float value) {
        //maxSpotAngle = 50 + (defaultSpotAngle * (value / 100));
        Debug.Log("value: " + value);
        //maxSpotAngle = -((7 * Mathf.Pow(value, 2)) / 250) + ((14 * value) / 5) + 20;
        maxSpotAngle = -((Mathf.Pow(value, 2) / 25)) + (4 * value) + 20;
        Debug.Log("angle: " + maxSpotAngle);
    }
}
