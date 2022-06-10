using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public int ScorePlayer1;
    public int ScorePlayer2;
    
    public Color colorPlayer1;
    public Color colorPlayer2;
    
    public float duration;
    
    public InputKeyboard inputKeyboardPlayer1;
    public InputKeyboard inputKeyboardPlayer2;
}
