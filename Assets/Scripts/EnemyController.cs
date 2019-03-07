using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed;
    public Health hp;
    public Rigidbody2D player;
    public Transform corpse;
    public Transform bullet;
    public Transform gunPivot;
    public Transform gun;

    private Rigidbody2D rb2d;
    private float agroDistance = 20;
    private bool agroed = false;
    private float time = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
            float distance = Mathf.Abs(Vector2.Distance(enemy_vec, player_vec));
            if (agroed || distance < agroDistance)
            {
                agroed = true;
                var ray = player_vec - enemy_vec;
                float angle = Mathf.Atan2(ray.y, ray.x);
                gunPivot.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
                ray.Normalize();
                rb2d.velocity = ray * maxSpeed;
                RaycastHit2D hit = Physics2D.Linecast(enemy_vec, player_vec);
                if (hit)
                {
                    if (hit.transform == player.transform)
                    {
                        time += 1;
                        if(time == 60)
                        {
                            time = 0;
                            var shoot = Instantiate(bullet, gun.position, gun.rotation);
                            shoot.tag = "EnemyBullet";
                            Debug.Log("Enemy Shooting");
                        }
                    }
                }
            }
        }
        else
        {
            Instantiate(corpse, new Vector3(rb2d.gameObject.transform.position.x + 1f, rb2d.gameObject.transform.position.y - 0.7f, rb2d.gameObject.transform.position.z), Quaternion.identity);
            Destroy(rb2d.gameObject);
        }
    }
}
