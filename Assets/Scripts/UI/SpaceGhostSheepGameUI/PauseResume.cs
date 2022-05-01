using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseResume : MonoBehaviour
{
   private bool isPaused;
   private GameObject _panel;
   private TextMeshProUGUI _pauseResumeText;

   void Start()
   {
      _pauseResumeText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
      _panel = GameObject.Find("DimPanel");
      _panel.SetActive(false);
   }

   

   //if game is paused this will resume it and vice versa
   public void PauseResumeGame()
   {
      if (!isPaused)
      {
         Time.timeScale = 0;
         _pauseResumeText.SetText("resume");
         isPaused = true;
         _panel.SetActive(true);
      }
      else
      {
         Time.timeScale = 1;
         isPaused = false;
         _pauseResumeText.SetText("pause");
         _panel.SetActive(false);
      }
   }
}
