using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

//this is an old class it is almost useless and will soon be deleted
//now only serves the purpose to choose the game duration
public class ControllColorChoice : MonoBehaviour
{
    [SerializeField]
    private GameDuration _gameDuration;
    private String _gameDurationInput;
    private const float MAX_GAME_DURATION = 300;
    private const float MIN_GAME_DURATION = 20;
    
    public InputKeyboard contrlPlayer1 = InputKeyboard.unchosen;
    public InputKeyboard contrlPlayer2 = InputKeyboard.unchosen;
    public Color colorPlayer1;
    public Color colorPlayer2;
    //public static ControllColorChoice choices;
    private Dropdown player1DropDown;
    private Dropdown player2DropDown;



    // Reads the game duration time from the input field and assigns it to the GameDuration ScriptableObject
    public void ReadGameDuration(String s)
    {
        float gameDuration = (float) Convert.ToDouble(s);

        if (gameDuration > MAX_GAME_DURATION) _gameDuration.duration = MAX_GAME_DURATION;
        else if (gameDuration < MIN_GAME_DURATION) _gameDuration.duration = MIN_GAME_DURATION;
       else  _gameDuration.duration = gameDuration;
    }


}
