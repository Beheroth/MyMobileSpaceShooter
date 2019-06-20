using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public float dropRate;

    public int scoreValue;

    protected GameController gameController;

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
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Boundary") || other.CompareTag("Boss") || other.CompareTag("Enemy") || other.CompareTag("Drop"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (dropRate > 0f)
        {
            GameObject drop = gameController.RollDrop(dropRate);
            if(drop!= null)
            {
                Instantiate(drop, transform.position, transform.rotation);
            }
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.setGameOver(true);
        }

        Destroy(gameObject);
        Destroy(other.gameObject);
        gameController.AddScore(scoreValue);
    }
}
