using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PawnSwitcher : MonoBehaviour
{
    public static PawnSwitcher instance;

    [SerializeField] CinemachineVirtualCamera virtual_cam;
    [SerializeField] private InputActionAsset action_asset;
    private InputActionMap car_input;
    private InputActionMap character_input;

    private GameObject character;



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
        character = GameObject.FindGameObjectWithTag("Player");

        character_input.Enable();
        car_input.Disable();
    }



    public void CharacterActive()
    {
        character_input.Enable();
        car_input.Disable();
        character.SetActive(true);
        SwitchToCharacterCamera();

    }
    public void CarActive(Transform target)
    {
        character_input.Disable();
        car_input.Enable();
        character.SetActive(false);
        SwitchToCarCamera(target);
    }

    void SwitchToCarCamera(Transform target)
    {
        virtual_cam.Follow = target;
        virtual_cam.transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    void SwitchToCharacterCamera()
    {
        virtual_cam.Follow = character.transform;
        virtual_cam.transform.rotation = Quaternion.Euler(45, 12, 0);
    }



}
