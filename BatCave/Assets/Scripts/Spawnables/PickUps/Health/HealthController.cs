using UnityEngine;
using System.Collections;
using System;

public class HealthController : MonoBehaviour {
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
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                EventManager.TriggerEvent(EventTypes.HEALTH_PICKED_UP);
                isHitByPlayer = true;
            }

            if (col.gameObject.tag == "CleanUp") {
                markedForDestroy = true;
                //gameObject.SetActive(false);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.position = new Vector2(-7.18f, 1.036f);
            }

            if (col.gameObject.tag == "Obstacle") {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2.5f);
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
        //gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.position = new Vector2(-7.18f, 1.036f);
    }
}
