using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCornerDoor : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
