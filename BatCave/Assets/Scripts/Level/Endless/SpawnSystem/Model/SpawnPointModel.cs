using UnityEngine;

public class SpawnPointModel : MonoBehaviour {
    
    private GameObject item;

    public void SetItem(GameObject item) {
        this.item = item;
    }

    public bool IsSlotTaken() {
        return this.item != null;
    }
}
