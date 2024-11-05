using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBrake : MonoBehaviour
{
    [SerializeField] private CarInputs carInputs;
    [SerializeField] private CarController carController;
    private Transform car;
    private Suspension suspension;
    private Transform tire;
    private Rigidbody rb;

    [Header("GasBrake")]

    private float gas_input;
    private bool hand_brake;
    private float available_tork;
    private Vector3 hand_brake_force = Vector3.zero;
    [SerializeField] private bool rear_wheel;
    [SerializeField] private float backward_acceleration;
    [SerializeField] private float top_speed;
    [SerializeField] private float acceleration;
    [SerializeField] private AnimationCurve power_curve;



    void Start()
    {
        car = transform.root;
        carController = car.GetComponent<CarController>();

        suspension = GetComponent<Suspension>();
        tire = suspension.tire;
        rb = suspension.rb;


    }


    void FixedUpdate()
    {
        if (carController.work)
        {
            if (suspension.rayDidHit)
            {
                gas_input = carInputs.gas_input;
                hand_brake = carInputs.hand_brake;


                Vector3 accel_direction = tire.forward;
                float car_speed = Vector3.Dot(car.forward, rb.velocity);
                float normalized_speed = Mathf.Clamp01(Mathf.Abs(car_speed) / top_speed);




                if (gas_input >= 0)
                { available_tork = power_curve.Evaluate(normalized_speed) * gas_input * acceleration; }
                else
                { available_tork = power_curve.Evaluate(normalized_speed) * gas_input * backward_acceleration; }


                if (hand_brake)
                {
                    available_tork = 0;

                    float forwardForceMagnitude = Vector3.Dot(rb.velocity, rb.transform.forward);
                    hand_brake_force = tire.forward * -forwardForceMagnitude * 150;

                    if (rear_wheel)
                    {
                        float rightForceMagnitude = Vector3.Dot(rb.velocity, rb.transform.right);
                        hand_brake_force += rightForceMagnitude * tire.right * 30;
                    }



                    hand_brake_force = Vector3.Lerp(hand_brake_force, Vector3.zero, 0.6f);




                }
                else
                {
                    hand_brake_force = Vector3.zero;
                }




                rb.AddForceAtPosition((available_tork * accel_direction) + hand_brake_force, tire.position);

            }
        }
    }
}
