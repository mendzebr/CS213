using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    private GameObject ghostSheep; 
    private GhostSheepBehavior gsB;

    public AudioSource source;
    public AudioClip getPointSound;
    void Start()
    {
        ghostSheep = GameObject.FindWithTag("GhostSheep");
        gsB = ghostSheep.GetComponent<GhostSheepBehavior>();
    }
    
     
        void OnTriggerEnter(Collider other){
        if (!gsB.isAttacking && other.transform.parent.gameObject.tag.Equals("GhostSheep"))
        {
            if (gsB.FindClosestPlayer().name == gsB.GetPlayer1.name)
                ++gsB.player1Score;
            
            else if (gsB.FindClosestPlayer().name == gsB.GetPlayer2.name)
                ++gsB.player2Score;
            
            source.PlayOneShot(getPointSound);
                
        }
     } 
     
}
