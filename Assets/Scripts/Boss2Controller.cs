using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class Boss2Controller : MonoBehaviour
{
    public Health hp;
    public Transform corpse;
    public Transform lazer;
    public Transform bullet;
    public Transform gunPivot;
    public Transform shotgun;
    public Transform enemy;
    public Transform wall;
    public Transform explosion;

    private Rigidbody2D rb2d;
    private Rigidbody2D player;

    private float gunCooldown = 0;
    private float maxSpeed = 2;
    private bool agroed = false;
    private bool shootingGun = false;
    private bool first_door = false;
    private bool second_door = true;
    private bool player_is_alive = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player.GetComponent<Health>().RemainingHP <= 0)
        {
            player_is_alive = false;
        }
    }

    void FixedUpdate()
    {
        if (hp.alive && player_is_alive)
        {
            Vector2 enemy_vec = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
            Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 enemy_to_player = player_vec - enemy_vec;
            if (!agroed)
            {
                float distance = enemy_to_player.sqrMagnitude;
                if (distance < 126)
                {
                    agroed = true;
                    if (first_door == false)
                    {
                        Instantiate(wall, new Vector3(16.25f, 2.08f, 0), Quaternion.identity);
                        first_door = true;
                        Debug.Log("wall!");
                    }
                }
            }
            if (agroed)
            {
                aimGun(shotgun, gunPivot);
                if (shootingGun == false)
                {
                    enemy_to_player.Normalize();
                    rb2d.velocity = enemy_to_player * maxSpeed;
                }
                //rb2d.transform.rotation = Quaternion.identity;
                handleShooting();
            }
        }
        else if (!hp.alive)
        {
            Instantiate(explosion, rb2d.transform.position, Quaternion.identity);
            Instantiate(corpse, rb2d.transform.position, Quaternion.Euler(0, 0, -10));
            Destroy(rb2d.gameObject);
            if (second_door == true)
            {
                Destroy(wall.gameObject);
                second_door = false;
            }
        }
    }

    private void shootGun(Transform bullet, Transform gun)
    {
        var shooting = Instantiate(bullet, gun.position, gun.rotation); //make it spawn at end of gun ?
        shooting.tag = "EnemyAttack";
    }

    private void aimGun(Transform gun, Transform gunPivot)
    {
        Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
        player_vec += player.velocity / 8; //woww this makes it tough
        Vector2 gun_vec = new Vector2(gun.transform.position.x, gun.transform.position.y);
        gun_vec = player_vec - gun_vec;

        float angle = Mathf.Atan2(gun_vec.y, gun_vec.x);
        gunPivot.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
    }

    private void handleShooting()
    {
        gunCooldown += 1;
        if (shootingGun)
        {
            if (gunCooldown > 70)
            {
                gunCooldown = 0;
                shootGun(bullet, shotgun);
            }
        }
    }
}
