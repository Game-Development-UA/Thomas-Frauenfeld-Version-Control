using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenController : MonoBehaviour {

    public int numHitsToKill = 20;
    public int hits = 0;
    public Rigidbody2D queen;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Projectile"))
        {
            hits += 1;
        }
    }

    public bool isDead()
    {
        return hits == numHitsToKill;
    }
}
