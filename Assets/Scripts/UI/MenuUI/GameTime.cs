using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{

    [SerializeField] private GameSettings gameSettings;
    
    private const float MAX_GAME_DURATION = 300;
    private const float MIN_GAME_DURATION = 20;
    
    
    public void ReadGameDuration(String s)
    {
        float gameDuration = (float) Convert.ToDouble(s);

        if (gameDuration > MAX_GAME_DURATION) gameSettings.duration = MAX_GAME_DURATION;
        else if (gameDuration < MIN_GAME_DURATION) gameSettings.duration = MIN_GAME_DURATION;
        else  gameSettings.duration = gameDuration;
    }

}
