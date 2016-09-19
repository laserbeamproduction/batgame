using UnityEngine;

public class SpawnPointModel {

    private Vector3 position;
    private GameObject item;

    public SpawnPointModel(Vector3 position) {
        this.position = position;
    }

    public void SetItem(GameObject item) {
        this.item = item;
    }

    public bool IsSlotTaken() {
        return this.item != null;
    }
}
