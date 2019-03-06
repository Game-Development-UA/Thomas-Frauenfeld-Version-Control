using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColliderScript : MonoBehaviour {

    bool canShoot = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Floor"))
        {
            canShoot = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Floor"))
        {
            canShoot = true;
        }
    }
    
    public bool GetIsCollision()
    {
        return canShoot;
    }
}
