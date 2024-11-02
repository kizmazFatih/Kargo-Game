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

                Vector3 accel_direction = tire.forward;
                float car_speed = Vector3.Dot(car.forward, rb.velocity);
                float normalized_speed = Mathf.Clamp01(Mathf.Abs(car_speed) / top_speed);
                float available_tork = power_curve.Evaluate(normalized_speed) * gas_input * acceleration;


                rb.AddForceAtPosition(available_tork * accel_direction * Time.fixedDeltaTime, tire.position);


                Debug.DrawRay(tire.position, available_tork * accel_direction, Color.red);

            }
        }

    }
}
