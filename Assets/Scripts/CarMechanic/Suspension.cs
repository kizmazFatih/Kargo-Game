using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    public Transform tire;
    public Rigidbody rb;
    [SerializeField] private LayerMask ignore_layer;
    [HideInInspector] public bool rayDidHit;

    [Header("Suspension")]
    [SerializeField] private float rest_distance;
    [SerializeField] private float travel_distance;
    private float min_lenght, max_lenght;
    [SerializeField] private float strenght;
    [SerializeField] private float damping;
    [SerializeField] private float tire_radius;







    void Start()
    {
        max_lenght = rest_distance + travel_distance;
        min_lenght = rest_distance - travel_distance;
    }

    void FixedUpdate()
    {

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, max_lenght + tire_radius, ~ignore_layer))
        {
            hit.distance = Mathf.Clamp(hit.distance, min_lenght, max_lenght + tire_radius);
            tire.position = hit.point + new Vector3(0, tire_radius, 0);

            if (hit.collider != null)
            {
                rayDidHit = true;

                float offset = rest_distance - hit.distance;
                Vector3 tire_dir = hit.transform.up;
                Vector3 tire_world_velocity = rb.GetPointVelocity(hit.point);

                float vel = Vector3.Dot(tire_dir, tire_world_velocity);

                float suspension_force = (offset * strenght) - (vel * damping);



                Debug.DrawRay(transform.position, -tire_dir * hit.distance, Color.green);
                rb.AddForceAtPosition(suspension_force * tire_dir * Time.fixedDeltaTime, tire.position);


            }

        }
        else
        {
            rayDidHit = false;
        }
    }


}
