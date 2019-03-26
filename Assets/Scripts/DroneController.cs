using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float maxSpeed;
    public Health hp;
    public Transform corpse;

    private Rigidbody2D rb2d;
    private Rigidbody2D player;

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
            float distance = Mathf.Abs(Vector2.Distance(drone_vec, player_vec));
            if(distance < 15)
            {
                drone_vec = player_vec - drone_vec;
                drone_vec.Normalize();
                rb2d.velocity = drone_vec * maxSpeed;
            }
        }
        else
        {
            Instantiate(corpse, new Vector3(rb2d.gameObject.transform.position.x + 1f, rb2d.gameObject.transform.position.y - 0.7f, rb2d.gameObject.transform.position.z), Quaternion.identity);
            Destroy(rb2d.gameObject);
        }
    }

  

}
