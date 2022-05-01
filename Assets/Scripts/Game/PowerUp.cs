using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class PowerUp : MonoBehaviour
{
    private float MIN_X =  1;
    private float MAX_X =  23;
    private float MIN_Z =  -18;
    private float MAX_Z =  -0.7f;
    private float Y = 0;

    private float respawnTime = 2f;
    
    public AudioSource source;
    public AudioClip collectedSound;
    public AudioClip spawnedSound;
    private string powerUpName = "powerUpNb1";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dog"))
            collectBy(other);
    }

    private void collectBy(Collider player)
    {
        source.PlayOneShot(collectedSound);
        Invoke("destroy",0.50f);
        MoveWithKeyboardBehavior behavior = player.GetComponent<MoveWithKeyboardBehavior>();
        behavior.powerUp(powerUpName,10);
    }

    private void destroy()
    {
        gameObject.SetActive(false);
        Invoke("respawn", respawnTime);
    }

    private void respawn()
    {
        float x = UnityEngine.Random.Range(MIN_X, MAX_X); 
        float y = Y; 
        float z = UnityEngine.Random.Range(MIN_Z, MAX_Z); 
        transform.position = new Vector3(x, y, z); 
        gameObject.SetActive(true);
        source.PlayOneShot(spawnedSound);

        
    }
    
}
