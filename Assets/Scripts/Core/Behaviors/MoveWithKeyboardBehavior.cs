using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//Input Keys
public enum InputKeyboard{
    arrows = 0, 
    wasd = 1,
    unchosen = 2
}

public enum Player
{
    player1 = 0,
    player2 = 1
}

public class MoveWithKeyboardBehavior : AgentBehaviour
{
    public Player player; 
    private bool canAttackOther;
    public InputKeyboard inputKeyboard;
    public AudioClip getAttackedByPlayer;
    public AudioSource source;

   // [SerializeField] private PlayersControl _playersControl;

    [SerializeField] private GameSettings gameSettings;


    public void Start()
    {
        if (player == Player.player1)
        {
            agent.SetVisualEffect(0, gameSettings.colorPlayer1, 100);
            inputKeyboard =gameSettings.inputKeyboardPlayer1;
        }

        if (player == Player.player2)
        {
            agent.SetVisualEffect(0, gameSettings.colorPlayer2, 100);
            inputKeyboard = gameSettings.inputKeyboardPlayer2;
        }
           
    }
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        if (inputKeyboard == InputKeyboard.arrows)
            steering.linear = new Vector3(Input.GetAxis("HorizontalArrows"), 0, Input.GetAxis("VerticalArrows")) *
                              agent.maxAccel;
        if (inputKeyboard == InputKeyboard.wasd)
            steering.linear = new Vector3(Input.GetAxis("HorizontalWASD"), 0, Input.GetAxis("VerticalWASD")) *
                              agent.maxAccel;
        steering.linear =
            this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));

        return steering;
    }
    public void powerUp(PowerUpType powerUpType)
    {
        GhostSheepBehavior gsB = 
            GameObject.FindWithTag("GhostSheep").GetComponent<GhostSheepBehavior>();
        
        switch (powerUpType)
        {
            case PowerUpType.coin:
                print("we in it");
                if (player == Player.player1)
                    ++gsB.player1Score;
                else
                    ++gsB.player2Score;
                break;
                
            case PowerUpType.activateGhost:
                gsB.chasePlayer(player == Player.player1 ? Player.player1 : Player.player2);
                break;
            case PowerUpType.canAttackOther:
                canAttackOther = this;
                Invoke("turnOffCanAttackOther",10);
                break;
        }
    }

    private void turnOffCanAttackOther()
    {
        canAttackOther = false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Dog") && canAttackOther)
        {
            source.PlayOneShot(getAttackedByPlayer);
            GameObject.FindGameObjectsWithTag("Dog");
            GameObject ghostSheep = GameObject.FindWithTag("GhostSheep");
            GhostSheepBehavior gsB = ghostSheep.GetComponent<GhostSheepBehavior>();
            // HOW TO ADD TO THE CORRECT PLAYER & SUB FROM THE CORRECT
            
            if (other.gameObject.name == gsB.GetPlayer1.name)
            {
                gsB.player1Score -= 2;
                gsB.player2Score += 2;
            }
            else
            {
                gsB.player1Score += 2;
                gsB.player2Score -= 2;
            }
        }
       
    }
}
