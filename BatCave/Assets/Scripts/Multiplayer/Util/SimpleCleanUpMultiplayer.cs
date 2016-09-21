using UnityEngine;
using System.Collections;

public class SimpleCleanUpMultiplayer : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "CleanUp" && Network.isServer)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
