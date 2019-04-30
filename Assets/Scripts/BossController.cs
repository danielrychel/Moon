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
    public Transform door;
    public Transform explosion;

    private Rigidbody2D rb2d;
    private Rigidbody2D player;

    private float pistolCooldown = 0;
    private float MGCooldown = 0;
    private float droneCooldown = 200;
    private float maxSpeed = 2;
    private float shootingTime = 0;
    private bool agroed = false;
    private bool shootingMG = false;
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
        if(player.GetComponent<Health>().RemainingHP <= 0)
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
                if(distance < 126)
                {
                    agroed = true;
                    if(first_door == false)
                    {
                        Instantiate(door, new Vector3(0, 25, 0), Quaternion.identity);
                        first_door = true;
                    }
                }
            }
            if (agroed)
            {
                aimGun(machineGun, MGPivot);
                aimGun(pistol, pistolPivot);
                if(shootingMG == false) //Only move when the machine gun isn't shooting
                {
                    enemy_to_player.Normalize();
                    rb2d.velocity = enemy_to_player * maxSpeed;
                }
                //rb2d.transform.rotation = Quaternion.identity;
                handleShooting();
                handleDrone();
            }
        }
        else if(!hp.alive)
        {
            Instantiate(explosion, rb2d.transform.position, Quaternion.identity);
            Instantiate(corpse, rb2d.transform.position, Quaternion.Euler(0,0,-10));
            Destroy(rb2d.gameObject);
            if(second_door == true)
            {
                Destroy(door.gameObject);
                second_door = false;
            }
        } 
        else if (!player_is_alive)
        {
            Vector2 enemy_vec = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
            Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 enemy_to_player = player_vec - enemy_vec;
            if(enemy_to_player.sqrMagnitude > 5)
            {
                rb2d.velocity = new Vector3(0, 0, 0);
                tauntDeath();
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
        if (gun == pistol)
        {
            player_vec += player.velocity / 8; //woww this makes it tough
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
            if(shootingTime > 250)
            {
                shootingMG = false;
                shootingTime = 0;
            }
        } else
        {
            if(shootingTime > 350)
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
            droneCooldown = 0;
            Instantiate(drone, new Vector3(12, 40, 0), Quaternion.identity);
        }
        
    }

    private void tauntDeath()
    {
        MGCooldown += 1;
        if(shootingMG == true)
        {
            MGPivot.rotation = MGPivot.rotation * Quaternion.Euler(0, 0, 1);
            shootGun(MGBullet, machineGun);
            if(MGCooldown > 360)
            {
                shootingMG = false;
                MGCooldown = 0;
            }
        }
        if(shootingMG == false && MGCooldown > 100)
        {
            shootingMG = true;
            MGCooldown = 0;
        }
    }
}