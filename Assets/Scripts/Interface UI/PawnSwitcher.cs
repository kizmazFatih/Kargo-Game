using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PawnSwitcher : MonoBehaviour
{
    [SerializeField] private InputActionAsset action_asset;
    private InputActionMap car_input;
    private InputActionMap character_input;

    private void Awake()
    {
        character_input = action_asset.FindActionMap("Character");
        car_input = action_asset.FindActionMap("Car");

        character_input.Enable();
        car_input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!car_input.enabled)
            {
                character_input.Disable();
                car_input.Enable();
            }
            else
            {
                character_input.Enable();
                car_input.Disable();
            }
        }
    }
}
