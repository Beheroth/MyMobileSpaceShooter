using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : DestroyByContact
{
    public int health;
    public AudioClip damageAudio;
    AudioSource audioSource;

    public int smoothing;

    public int zpos;

    private Rigidbody rb;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Couldn't find 'GameController' script");
        }
        this.rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.position.z, zpos, Time.deltaTime * smoothing);
        rb.position = new Vector3(rb.position.x, 0.0f, newManeuver);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Boss") || other.CompareTag("Enemy") || other.CompareTag("Drop"))
        {
            return;
        }

        if (other.CompareTag("Player")){return;}    //GODMODE

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }

        this.health--;
        Destroy(other.gameObject);
        if (damageAudio != null)
        {
            audioSource.PlayOneShot(damageAudio, damageAudio.length);
        }

        if (health <= 0)
        {
            Die();
            gameController.AddScore(scoreValue);
        }
    }

    private void Die()
    {
        gameController.setBossFight(false);
        Destroy(gameObject);
        if (dropRate > 0f)
        {
            GameObject drop = this.gameController.RollDrop(dropRate);
            Instantiate(drop, transform.position, transform.rotation);
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}