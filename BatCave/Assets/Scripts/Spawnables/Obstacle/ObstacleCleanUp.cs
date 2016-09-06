using UnityEngine;
using System.Collections;

public class ObstacleCleanUp : MonoBehaviour {
    public bool markedForDestroy = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "CleanUp")
        {
            markedForDestroy = true;
            gameObject.SetActive(false);
        }
    }
}
