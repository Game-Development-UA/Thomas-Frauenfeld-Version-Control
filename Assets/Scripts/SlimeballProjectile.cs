using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeballProjectile : MonoBehaviour {

    public Rigidbody2D rbody;
    public float speed;
    public float lifetime;
    public PlayerController player;

    float timer;
	
	void Update () {
        timer += Time.deltaTime;

        if (timer > lifetime)
        {
            player.ProjectileDestroyed(this);
            Destroy(this.gameObject);
        }
	}

    void FixedUpdate()
    {
        rbody.velocity = rbody.transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider2D>())
        {
            Destroyable destroyable = collision.gameObject.GetComponent<Destroyable>();

            if (destroyable)
            {
                Destroy(destroyable.gameObject);
            }
            player.ProjectileDestroyed(this);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == ("Floor"))
        {
            player.ProjectileDestroyed(this);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == ("Queen") && collision.gameObject.GetComponent<QueenController>().isDead())
        {
            Destroy(collision.gameObject);
        }
    }
}
