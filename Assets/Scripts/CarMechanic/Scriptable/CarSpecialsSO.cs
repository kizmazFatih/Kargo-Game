using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarSpecialsSO", menuName = "ScriptableObjects/AddCarSpecials", order = 1)]
public class CarSpecialsSO : ScriptableObject
{
    public string car_name;
    public int capacity;

    [Range(0, 100)]
    public float fuel;

}
