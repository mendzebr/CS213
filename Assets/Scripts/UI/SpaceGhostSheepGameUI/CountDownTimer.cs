using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField]
    private GameDuration _gameDuration;
    
    
    private GameObject scoreText;
    public TextMeshProUGUI textUI;
    float currentTime = 0;
    [SerializeField]
    //float startingTime = ControllColorChoice.GameDuration;
    public GhostSheepBehavior gsB;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = _gameDuration.duration;
        //currentTime = ControllColorChoice.GameDuration;
        //print(ControllColorChoice.GameDuration);

        scoreText = GameObject.Find("CDTimer");
        textUI = scoreText.GetComponent<TextMeshProUGUI>();
//        print(textUI);
        
        
    }

    // Update is called once per frame
    void Update()
    {

        currentTime -= 1 * Time.deltaTime;
        textUI.SetText(currentTime.ToString("0"));
        //print(currentTime.ToString());
        if (currentTime <= 5+Time.deltaTime)
        {
            textUI.color = Color.red;
        }
        if (currentTime <= 0)
        {
            currentTime = 0;
            ScoreKeeper.sk.sc1 = gsB.player1Score;
            ScoreKeeper.sk.sc2 = gsB.player2Score;
            SceneManager.LoadScene("ScoreScene");
        }

    }
    
}
