using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GhostSheepBehavior boh;
    public TextMeshProUGUI scorePlayer1;
    public TextMeshProUGUI scorePlayer2;


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        scorePlayer1.text = boh.player1Score.ToString();
        scorePlayer2.text = boh.player2Score.ToString();
    }
}
