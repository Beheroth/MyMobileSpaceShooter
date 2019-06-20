using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform[] shotSpawns;
    public float firerate;
    public float delay;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, firerate);
    }

    void Fire ()
    {
        foreach(Transform shotspawn in shotSpawns)
        {
            Instantiate(shot, shotspawn.position, shotspawn.rotation);
            audioSource.Play();
        }
    }
}
