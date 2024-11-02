using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField] private CarInputs carInputs;
    [SerializeField] private CarController carController;
    private Suspension suspension;
    private Transform tire;
    private Rigidbody rb;

    [Header("Steering")]
    [SerializeField] private float tire_grip_factor;
    [SerializeField] private bool turnable;
    private float turn_input;
    [SerializeField] private AnimationCurve turn_power_curve;


    void Start()
    {
        carController = transform.root.GetComponent<CarController>();
        suspension = GetComponent<Suspension>();

        tire = suspension.tire;
        rb = suspension.rb;
    }


    void FixedUpdate()
    {

        if (carController.work)
        {

            if (turnable)
            {
                TurnToTire();
            }



            if (suspension.rayDidHit)
            {

                Vector3 steering_direction = tire.right;
                Vector3 tire_world_velocity = rb.GetPointVelocity(tire.position);

                float steering_velocity = Vector3.Dot(steering_direction, tire_world_velocity);
                float desired_velocity_change = -steering_velocity * tire_grip_factor;

                float desired_acceleration = desired_velocity_change / Time.fixedDeltaTime;


                rb.AddForceAtPosition(desired_acceleration * steering_direction * 50*Time.fixedDeltaTime, tire.position);


            }
        }





    }

    void TurnToTire()
    {

        turn_input = carInputs.turn_input;
        float normalized_speed = Mathf.Clamp01(Mathf.Abs(rb.velocity.magnitude) / 40);
        float tire_angle = turn_input * 25 * turn_power_curve.Evaluate(normalized_speed);
        tire.localRotation = Quaternion.Euler(0, tire_angle, tire.localEulerAngles.z);
    }
}
