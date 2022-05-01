using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{ 
    public int sc1;
    public int sc2;
    public static ScoreKeeper sk;

    // Start is called before the first frame update
    void Awake()
    {
        if (sk == null)
        {
            sk = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(this); 

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
