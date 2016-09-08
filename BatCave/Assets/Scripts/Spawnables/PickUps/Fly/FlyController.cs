using UnityEngine;
using System.Collections;
using System;

public class FlyController : MonoBehaviour {
    public bool markedForDestroy = false;
    private ParticleSystem particle;
    private SpriteRenderer spriteRenderer;

    private bool isHitByPlayer;
    private bool isEmittingBlood;

    void Start() {
        particle = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (isHitByPlayer) {
            if (!isEmittingBlood) {
                StartBloodParticles();
                isEmittingBlood = true;
                spriteRenderer.enabled = false;
            } else {
                if (!particle.isPlaying) {
                    OnParticleAnimFinished();
                }
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col) {
        if (!isHitByPlayer) {
            if (col.gameObject.tag == "Player") {
                EventManager.TriggerEvent(EventTypes.FLY_PICK_UP);
                isHitByPlayer = true;
            }

            if (col.gameObject.tag == "CleanUp") {
                markedForDestroy = true;
                gameObject.SetActive(false);
            }
        }
    }

    void StartBloodParticles() {
        particle.Play();
    }

    void OnParticleAnimFinished() {
        markedForDestroy = true;
        spriteRenderer.enabled = true;
        isHitByPlayer = false;
        isEmittingBlood = false;
        gameObject.SetActive(false);
    }
}
