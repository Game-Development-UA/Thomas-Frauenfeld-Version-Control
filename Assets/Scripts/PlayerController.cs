using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D player;
    public float playerSpeed;
    public float jumpPower;
    public GameObject slimeBall;
    public float slimeBallSpeed;
    public int ammo = 10;
    public int health = 100;

    float horizontal;
    bool grounded;

    void Start() {
        player = GetComponent<Rigidbody2D>();
        grounded = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            player.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
            grounded = false;
        }

        horizontal = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(horizontal * playerSpeed, player.velocity.y);

        if (horizontal < 0)
        {
            transform.right = new Vector3(-1f, 0f, 0f);
        }
        else if (horizontal > 0)
        {
            transform.right = new Vector3(1f, 0f, 0f);
        }

        if (ammo > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject shot = (GameObject)(Instantiate(slimeBall, transform.position + transform.right * 1.5f, Quaternion.identity));
                shot.GetComponent<Rigidbody2D>().AddForce(transform.right * slimeBallSpeed);
                ammo--;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Floor") && grounded == false)
        {
            grounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        AmmoPowerup ammoPowerup = collider.gameObject.GetComponent<AmmoPowerup>();
        HealthPowerup healthPowerup = collider.gameObject.GetComponent<HealthPowerup>();

        if (ammoPowerup != null)
        {
            ammo += ammoPowerup.ammoIncrease;
            Destroy(ammoPowerup.gameObject);
        }

        if (healthPowerup != null)
        {
            if (health == 50)
            {
                health += healthPowerup.healthIncrease;
                Destroy(healthPowerup.gameObject);
            }
        }
    }
}