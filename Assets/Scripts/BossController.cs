using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class BossController : MonoBehaviour
{
    public Health hp;
    public Transform corpse;
    public Transform pistolBullet;
    public Transform MGBullet;
    public Transform pistolPivot;
    public Transform pistol;
    public Transform MGPivot;
    public Transform machineGun;
    public Transform drone;

    private Rigidbody2D rb2d;
    private Rigidbody2D player;

    private float pistolCooldown = 0;
    private float MGCooldown = 0;
    private float droneCooldown = 200;
    private float maxSpeed = 2;
    private float shootingTime = 0;
    private bool agroed = false;
    private bool shootingMG = false;

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
            Vector2 enemy_vec = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
            Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 enemy_to_player = player_vec - enemy_vec;
            if (!agroed)
            {
                float distance = Mathf.Abs(Vector2.Distance(enemy_vec, player_vec));
                RaycastHit2D see = Physics2D.Linecast(enemy_vec, player_vec);
                if(distance < 10 && see.transform.tag == "Player")
                {
                    agroed = true;
                }
                else if(see.transform.tag != "Player")
                {
                    agroed = false;
                }
            }
            if (agroed)
            {
                aimGun(machineGun, MGPivot);
                aimGun(pistol, pistolPivot);
                //handleDrone();
                RaycastHit2D hit = Physics2D.Linecast(pistol.position, player_vec); //check if the enemy can see the player
                if (hit.transform.tag == "Player") //If it can see the player, then shoot at it 
                { //probably able to get rid of this if statement but not sure yet
                    enemy_to_player.Normalize();
                    rb2d.velocity = enemy_to_player * maxSpeed;
                    rb2d.transform.rotation = Quaternion.identity;
                    handleShooting();
                }
            }
        }
        else
        {
            Instantiate(corpse, new Vector3(rb2d.gameObject.transform.position.x + 1f, rb2d.gameObject.transform.position.y - 0.7f, rb2d.gameObject.transform.position.z), Quaternion.identity);
            Destroy(rb2d.gameObject);
        }
    }

    private void shootGun(Transform bullet, Transform gun)
    {
        //if(gun == machineGun)
        {
            var shooting = Instantiate(bullet, gun.position, gun.rotation); //make it spawn at end of gun
            shooting.tag = "EnemyAttack";
        }
        /*else
        {
            var shooting = Instantiate(bullet, gun.position, gun.rotation);
            shooting.tag = "EnemyAttack";
        }*/
        
    }

    private void aimGun(Transform gun, Transform gunPivot)
    {
        Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
        if (gun == pistol)
        {
            player_vec += player.velocity/8; //woww this makes it really tough!
        }
        Vector2 gun_vec = new Vector2(gun.transform.position.x, gun.transform.position.y);
        gun_vec = player_vec - gun_vec;

        float angle = Mathf.Atan2(gun_vec.y, gun_vec.x);
        gunPivot.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
    }

    private void handleShooting()
    {
        pistolCooldown += 1;
        if (pistolCooldown > 70)
        {
            pistolCooldown = 0;
            shootGun(pistolBullet, pistol);
        }
        shootingTime += 1;
        MGCooldown += 1;
        if (shootingMG)
        {
            if(MGCooldown > 3)
            {
                MGCooldown = 0;
                shootGun(MGBullet, machineGun);
            }
            if(shootingTime > 200)
            {
                shootingMG = false;
                shootingTime = 0;
            }
        } else
        {
            if(shootingTime > 300)
            {
                shootingMG = true;
                shootingTime = 0;
            }
        }
    }

    private void handleDrone()
    {
        droneCooldown += 1;
        if(droneCooldown > 1000)
        {
            Instantiate(drone, new Vector3(12, 40, 0), Quaternion.identity);
            droneCooldown = 0;
        }
    }
}