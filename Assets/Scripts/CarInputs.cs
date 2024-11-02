using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CarInputs : MonoBehaviour
{


    [HideInInspector] public float gas_input;
    [HideInInspector] public float turn_input;
    [HideInInspector] public float hand_brake;


    public void CarMove(InputAction.CallbackContext context)
    {
        gas_input = context.ReadValue<Vector2>().y;
        turn_input = context.ReadValue<Vector2>().x;
    }

    public void HandBrake(InputAction.CallbackContext context)
    {
        if (context.started || context.performed) hand_brake = 1;
        else { hand_brake = 0; }
    }
}
