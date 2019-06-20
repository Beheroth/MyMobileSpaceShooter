using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Red : MonoBehaviour
{
    public float rofMult;

    private PlayerController playerController;
    // Start is called before the first frame update

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Couldn't find 'PlayerController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this + " collide with" + other);
        if (other.CompareTag("Player"))
        {
            float fireDelta = playerController.getFireDelta();
            playerController.setFireDelta(fireDelta / rofMult);
            Destroy(gameObject);
        }
    }
}
