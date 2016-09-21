using UnityEngine;
using System.Collections;

public class SimpleCleanUpMultiplayer : MonoBehaviour {


    void OnCollisionEnter2D(Collision2D col)
    {
        //This should only be done by the server
        if (col.gameObject.name == "CleanUp" && Network.isServer)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
