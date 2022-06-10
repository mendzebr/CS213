using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nTouchBorders : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    public GameObject player1;
    public GameObject player2;

    public MoveWithKeyboardBehavior player1Mover;
    public MoveWithKeyboardBehavior player2Mover;

    public GhostSheepBehavior gsB;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject ghostSheep = GameObject.FindWithTag("GhostSheep");
        gsB = ghostSheep.GetComponent<GhostSheepBehavior>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            if (collision.gameObject == player1)
            {
                --gsB.player1Score;

            }

            if (collision.gameObject == player2)
            {
                --gsB.player2Score;
            }
                
        }
    }
}
