using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PawnSwitcher : MonoBehaviour
{
    public static PawnSwitcher instance;
    [SerializeField] private InputActionAsset action_asset;
    private InputActionMap car_input;
    private InputActionMap character_input;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }


        character_input = action_asset.FindActionMap("Character");
        car_input = action_asset.FindActionMap("Car");

        character_input.Enable();
        car_input.Disable();
    }



    public void CharacterActive()
    {
        character_input.Enable();
        car_input.Disable();
    }
    public void CarActive()
    {
        character_input.Disable();
        car_input.Enable();
    }

}
