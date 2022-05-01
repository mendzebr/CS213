using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllDropdown : MonoBehaviour
{
    public Player player;
    public Dropdown dropdown;
    [SerializeField] private PlayersControl _playersControl;
    
    // Start is called before the first frame update
    private void Awake()
    { 
        dropdown = transform.GetComponent<Dropdown>();
    }

    void Start()
    {
        dropdown.options.Clear();
        List<string> controlls = new List<string>();
        controlls.Add("WASD");
        controlls.Add("Arrows");
        foreach (var controll in controlls)
            dropdown.options.Add(new Dropdown.OptionData(){text = controll});
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
        
        if (player == Player.player1) 
            dropdown.value = 0;
        
        if (player == Player.player2)
            dropdown.value = 1;
       
        DropdownItemSelected(dropdown);
    } 
    public void DropdownItemSelected(Dropdown dropdown)
    {
        string controllSelected = dropdown.options[dropdown.value].text;
        InputKeyboard controll = InputKeyboard.unchosen;
        switch (controllSelected)
        {
            case "WASD":
                controll = InputKeyboard.wasd;
                break;
            case "Arrows":
                controll = InputKeyboard.arrows;
                break;
        }

        if (player == Player.player1)
            _playersControl.inputKeyboardPlayer1 = controll;
        if (player == Player.player2)
            _playersControl.inputKeyboardPlayer2 = controll;
    }
}
