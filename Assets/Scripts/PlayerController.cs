using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D player;
    public Rigidbody2D queen;
    public float playerSpeed;
    public float jumpPower;
    public int maxProjectiles;
    public Animator Animation;
    public float runThreshold;
    public GameObject startSpawnLocation;
    public GameObject midpointSpawnLocation;

    public Transform slimeballSpawnLocation;
    public SlimeballProjectile slimeballPrefab;
    public EnemyController redAnt;
    public GameObject queenSpawner;
    public List<SlimeballProjectile> slimeballs = new List<SlimeballProjectile>();
    public AudioClip shootSoundEffect;
    public AudioClip jumpSoundEffect;

    float horizontal;
    bool grounded;
    int health;
    public int lives;
    public int coins;

    void Start() {
        health = 50;
        grounded = true;
        lives = 3;
        player.transform.position = startSpawnLocation.transform.position;
    }

    void Update() {
        horizontal = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(horizontal * playerSpeed, player.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            player.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
            AudioSource.PlayClipAtPoint(jumpSoundEffect, transform.position);
        }

        if (horizontal < 0)
        {
            transform.right = new Vector3(-1f, 0f, 0f);
        }
        else if (horizontal > 0)
        {
            transform.right = new Vector3(1f, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !slimeballSpawnLocation.GetComponent<SpawnColliderScript>().GetIsCollision())
        {
            if (slimeballs.Count < maxProjectiles)
            {
                SlimeballProjectile newSlimeball = Instantiate<SlimeballProjectile>(slimeballPrefab, slimeballSpawnLocation.position, player.transform.rotation);
                AudioSource.PlayClipAtPoint(shootSoundEffect, transform.position);
                newSlimeball.transform.position = slimeballSpawnLocation.position;
                newSlimeball.player = this;

                slimeballs.Add(newSlimeball);
            }
        }

        if (coins == 100)
        {
            lives += 1;
            coins = 0;
        }

        Animation.SetBool("Running", Mathf.Abs(player.velocity.x) > runThreshold);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Floor") && grounded == true)
        {
            grounded = false;
            Animation.SetBool("Jumping", true);
        }

        else if (collision.gameObject.tag == ("Enemy"))
        {
            Animation.SetBool("TakingDamage", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Floor"))
        {
            grounded = true;
            Animation.SetBool("Jumping", false);
        }

        else if (collision.gameObject.tag == ("Enemy"))
        {
            Animation.SetBool("TakingDamage", true);

            if (health == 100)
            {
                health -= 50;
                player.transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f);
            }
            else
            {
                lives -= 1;

                if (lives >= 0)
                {
                    if (player.transform.position.x < 68)
                    {
                        player.transform.position = startSpawnLocation.transform.position;
                    }
                    else
                    {
                        player.transform.position = midpointSpawnLocation.transform.position;
                    }
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == ("HealthPowerup") && health == 50)
        {
            health += 50;
            Destroy(collider.gameObject);
            player.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
        }

        else if (collider.gameObject.tag == ("LifePowerup"))
        {
            lives += 1;
            Destroy(collider.gameObject);
        }

        else if (collider.gameObject.tag == ("Coin"))
        {
            coins += 1;
            Destroy(collider.gameObject);
        }

        else if (collider.gameObject.tag == ("HoleCollider1"))
        {
            if (lives > 0)
            {
                lives -= 1;
                player.transform.position = startSpawnLocation.transform.position;
            }
            else
            {
                SceneManager.LoadScene(3);
            }
        }

        else if (collider.gameObject.tag == ("HoleCollider2"))
        {
            if (lives > 0)
            {
                lives -= 1;
                player.transform.position = midpointSpawnLocation.transform.position;
            }
        }

        else if (collider.gameObject.tag == ("QueenSpawnLocation"))
        {
            Instantiate<EnemyController>(redAnt, queenSpawner.transform.position, queen.transform.rotation);
            Instantiate<EnemyController>(redAnt, queenSpawner.transform.position, queen.transform.rotation);
            Instantiate<EnemyController>(redAnt, queenSpawner.transform.position, queen.transform.rotation);
        }

        else if (collider.gameObject.tag == ("EndLevel"))
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ProjectileDestroyed( SlimeballProjectile destroyedProjectile)
    {
        slimeballs.Remove(destroyedProjectile);
    }
}