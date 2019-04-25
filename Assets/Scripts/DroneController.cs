using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float maxSpeed;
    public Health hp;
    public Transform explosion;

    private Rigidbody2D rb2d;
    private Rigidbody2D player;
    private bool agroed = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (hp.alive)
        {
            Vector2 drone_vec = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
            Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
            if (!agroed)
            {
                Vector2 drone_to_player = player_vec - drone_vec;
                float distance = drone_to_player.sqrMagnitude;
                if(distance < 900)
                {
                    RaycastHit2D see = Physics2D.Linecast(drone_vec, player_vec); //check if the enemy can see the player
                    if(see.transform.tag == "Player")
                    {
                        agroed = true;
                    }
                }
            }
            if (agroed)
            {
                drone_vec = player_vec - drone_vec;
                drone_vec.Normalize();
                rb2d.velocity = drone_vec * maxSpeed;
            }
        }
        else
        {
            Instantiate(explosion, rb2d.transform.position, Quaternion.identity);
            Destroy(rb2d.gameObject);
        }
    }

  

}
