using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawnL;
    public Transform shotSpawnR;
    private Transform shotSpawn;
    public float fireDelta = 0.5F;
    private float nextFire = 0.5F;
    private GameObject newProjectile;

    private float myTime = 0.0F;

    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.y = 0f;
            //Debug.Log(touchPosition);

            float zpos = Mathf.MoveTowards(rb.position.z, touchPosition.z, Time.deltaTime*speed);
            float xpos = Mathf.MoveTowards(rb.position.x, touchPosition.x, Time.deltaTime*speed);
            //Debug.Log(zpos);

            //rb.position = touchPosition;
            rb.position = new Vector3(xpos, 0.0f, zpos);
        }

        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            if (shotSpawn == shotSpawnL)
            {
                shotSpawn = shotSpawnR;
            }
            else
            {
                shotSpawn = shotSpawnL;
            }
            nextFire = myTime + fireDelta;
            newProjectile = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;

            // create code here that animates the newProjectile 

            nextFire = nextFire - myTime;
            myTime = 0.0F;
			GetComponent<AudioSource>().Play();
        }
    }

    /*
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x* -tilt);
    }
    */

    public float getFireDelta()
    {
        return this.fireDelta;
    }

    public void setFireDelta(float fireDelta)
    {
        this.fireDelta = fireDelta;
        this.nextFire = this.fireDelta;
        //Debug.Log(this.fireDelta);
    }
}
