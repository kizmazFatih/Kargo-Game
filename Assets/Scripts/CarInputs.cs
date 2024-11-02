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
    [HideInInspector] public bool hand_brake;


    public void CarMove(InputAction.CallbackContext context)
    {
        gas_input = context.ReadValue<Vector2>().y;
        turn_input = context.ReadValue<Vector2>().x;
    }

    public void HandBrake(InputAction.CallbackContext context)
    {
        hand_brake = context.started || context.performed;

    }
}
