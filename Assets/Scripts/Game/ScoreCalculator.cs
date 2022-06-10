using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    public UnityEngine.UI.Text score;

    private void Awake()
    {
       TryGetComponent(out score);
    }
    void Start()
    {
        
        int score1 = gameSettings.ScorePlayer1;
        int score2 = gameSettings.ScorePlayer2;
        if (score1 == score2)
            score.text = "It's a draw with: "+ score1.ToString()+" points";
            
        else if (score1 > score2)
            score.text =  $"Player 1 has won with: { score1 } points";
        else
            score.text = "Player 2 has won with: "+score2.ToString()+" points";
    }
    
}
