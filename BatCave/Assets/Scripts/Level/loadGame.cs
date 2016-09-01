using UnityEngine;
using System.Collections;

public class loadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void restartGame() {
        Application.LoadLevel("scene_One");
    }
}
