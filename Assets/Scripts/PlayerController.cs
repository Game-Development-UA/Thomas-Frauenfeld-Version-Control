using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D player;
    public float playerSpeed;
    public float jumpPower;
    public int maxProjectiles;

    public Transform slimeballSpawnLocation;
    public SlimeballProjectile slimeballPrefab;
    public List<SlimeballProjectile> slimeballs = new List<SlimeballProjectile>();

    public int health = 100;
    public string direction;

    float horizontal;
    bool grounded;

    void Start() {
        grounded = false;
    }

    void Update() {
        horizontal = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(horizontal * playerSpeed, player.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            player.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
            grounded = false;
        }

        if (horizontal < 0)
        {
            transform.right = new Vector3(-1f, 0f, 0f);
            direction = "left";
        }
        else if (horizontal > 0)
        {
            transform.right = new Vector3(1f, 0f, 0f);
            direction = "right";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (slimeballs.Count < maxProjectiles)
            {
                SlimeballProjectile newSlimeball = Instantiate<SlimeballProjectile>(slimeballPrefab);
                newSlimeball.transform.position = slimeballSpawnLocation.position;
                newSlimeball.player = this;

                slimeballs.Add(newSlimeball);
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
        HealthPowerup healthPowerup = collider.gameObject.GetComponent<HealthPowerup>();

        if (healthPowerup != null)
        {
            if (health == 50)
            {
                health += healthPowerup.healthIncrease;
            }
            Destroy(healthPowerup.gameObject);
        }
    }

    public void ProjectileDestroyed( SlimeballProjectile destroyedProjectile)
    {
        slimeballs.Remove(destroyedProjectile);
    }
}