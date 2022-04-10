using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//Input Keys
public enum InputKeyboard{
    arrows = 0, 
    wasd = 1
}

public class MoveWithKeyboardBehavior : AgentBehaviour
{
    
    public InputKeyboard inputKeyboard;
    public void Start()
    {
        if (inputKeyboard == InputKeyboard.arrows)
            agent.SetVisualEffect(0, Color.blue, 100);

        if (inputKeyboard == InputKeyboard.wasd) 
            agent.SetVisualEffect(0,Color.yellow,100);
    }
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        if (inputKeyboard == InputKeyboard.arrows)
            steering.linear = new Vector3(Input.GetAxis("HorizontalArrows"), 0, Input.GetAxis("VerticalArrows")) *agent.maxAccel;
        if(inputKeyboard == InputKeyboard.wasd)
            steering.linear = new Vector3(Input.GetAxis("HorizontalWASD"), 0, Input.GetAxis("VerticalWASD")) *agent.maxAccel;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
            linear , agent.maxAccel)) ;
        
        return steering;
    }

}
