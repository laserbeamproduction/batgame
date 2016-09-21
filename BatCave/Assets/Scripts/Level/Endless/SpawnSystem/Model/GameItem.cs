using UnityEngine;
using System.Collections;

public class GameItem : MonoBehaviour {

    [Range(0f, 100f)]
    public float spawnChance;

    [Range(1, 5)]
    public int laneWeight;

    private bool isAvailable = true;

    public bool IsAvailable() {
        return this.isAvailable;
    }

    public void SetAvailable(bool available) {
        this.isAvailable = available;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "CleanUp") {
            SetAvailable(true);
        }
    }
}
