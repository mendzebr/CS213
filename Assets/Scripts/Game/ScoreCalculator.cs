using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCalculator : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Text score;

    private void Awake()
    {
       TryGetComponent(out score);
    }
    void Start()
    {
        int score1 = ScoreKeeper.sk.sc1;
        int score2 = ScoreKeeper.sk.sc2;
        if (score1 == score2)
            score.text = "It's a draw with: "+ score1.ToString()+" points";
            
        else if (score1 > score2)
            score.text =  $"Player 1 has won with: { score1 } points";
        else
            score.text = "Player 2 has won with: "+score2.ToString()+" points";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
