using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{
    private Toggle myToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        myToggle = GetComponent<Toggle>();
        if (AudioListener.volume == 0)
            myToggle.isOn = false;
    }

    public void ToggleAudioOnVolumeChange(bool audioIn)
    {
        if (audioIn)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
    }
}
