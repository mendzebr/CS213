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
    private bool isPoweredUp;
    public InputKeyboard inputKeyboard;
    public AudioClip getAttackedByPlayer;
    public AudioSource source;

    [SerializeField] private PlayersColor _playersColor;
    [SerializeField] private PlayersControl _playersControl;
    
    /*
    [SerializeField]
    private GameObject _player1;
    [SerializeField]
    private GameObject _player2;
    public GameObject GetPlayer1 => _player1;
    public GameObject GetPlayer2 => _player2;
    */

    public void Start()
    {
        if (player == Player.player1)
        {
            agent.SetVisualEffect(0, _playersColor.colorPlayer1, 100);
            inputKeyboard =_playersControl.inputKeyboardPlayer1;
        }

        if (player == Player.player2)
        {
            agent.SetVisualEffect(0, _playersColor.colorPlayer2, 100);
            inputKeyboard = _playersControl.inputKeyboardPlayer2;
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
    
    public void powerUp(string powerUpName, float length)
    {
        isPoweredUp = true;
        Invoke("normalize"+powerUpName , length);
    }

    private void normalizepowerUpNb1()
    {
        isPoweredUp = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Dog") && isPoweredUp)
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
