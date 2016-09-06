﻿using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {
    public float stamina;
    public float maxStamina;
    public int echoUsedAmount;

    public int echoCost;
    public int batResourcePickup;
    //public int draculaResourcePickup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (stamina < 0) {
            stamina = 0;
        }
	}

    public void addStamina(int value) {
        stamina += value;
    }

    public void removeStamina(int value) {
        stamina -= value;
    }

    public void echoUsed() {
        echoUsedAmount += 1;
    }

    //public void humanEaten(int value) {
    //    stamina += value;
    //}
}
