using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorDropdown : MonoBehaviour
{
    public Player player;
    public Dropdown dropdown;

    [SerializeField] private PlayersColor _playersColor;
    
    // Start is called before the first frame update
   

    void Start()
    {
        dropdown.options.Clear();
        List<string> colors = new List<string>();
        colors.Add("Red");
        colors.Add("Blue");
        colors.Add("Magenta");
        colors.Add("Yellow");
        foreach (var color in colors)
            dropdown.options.Add(new Dropdown.OptionData(){text = color});
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
        
        if (player == Player.player1)
            dropdown.value = 0;
        if (player == Player.player2)
            dropdown.value = 1;
        DropdownItemSelected(dropdown);
        
    }

    public void DropdownItemSelected(Dropdown dropdown)
    {
        string colorSelected = dropdown.options[dropdown.value].text;
        Color color = Color.blue;
        switch (colorSelected)
        {
            case "Red":
                color = Color.red;
                break;
            case "Blue":
                color = Color.blue;
                break;
            case "Magenta":
                color = Color.magenta;
                break;
            case "Yellow":
                color = Color.yellow;
                break;
        }

        if (player == Player.player1)
            _playersColor.colorPlayer1 = color;
        if (player == Player.player2)
            _playersColor.colorPlayer2 = color;
    }
   
}
