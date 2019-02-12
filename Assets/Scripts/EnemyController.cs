using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Rigidbody2D rbody;
    public Transform floorSensor;
    public float floorSensorRange;
    public float speed;
    public bool isDead;

    void Start()
    {
        isDead = false; 
    }

    void Update()
    {
        RaycastHit2D floorHit = Physics2D.Raycast(floorSensor.position, floorSensor.right, floorSensorRange);

        if (floorHit.collider)
        {
            Floor floor = floorHit.collider.GetComponent<Floor>();
            if (!floor)
            {
                TurnAround();
            }
        }
        else
        {
            TurnAround();
        }

        // if isDead
        // This block should destroy the ant gameObject
    }

    void FixedUpdate()
    {
        Vector3 vel = rbody.velocity;
        vel.x = transform.right.x * speed;
        rbody.velocity = vel;
    }

    void TurnAround()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0f, 180f, 0f);
    }

    // OnTriggerEnter (I think)
    // In this method, The boolean isDead should be changed to true

    /*
     * I'm not sure how to handle the player jumping directly on top of the enemy
     * in order to kill it, but I think I need a collision handler here.
     */
}
