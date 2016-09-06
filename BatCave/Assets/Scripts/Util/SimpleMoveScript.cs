using UnityEngine;
using System.Collections;

public class SimpleMoveScript : MonoBehaviour {

    public float speed;
    public Vector3 direction;

    // Update is called once per frame
    void FixedUpdate() {
        this.gameObject.transform.Translate(direction * speed * Time.deltaTime);
    }
}
