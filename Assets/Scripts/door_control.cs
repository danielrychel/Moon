using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_control : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Rigidbody2D player;
    public SpriteRenderer sprite;
    private Color orig_color;
    public bool switched;
    // Start is called before the first frame update
    void Start()
    {
        orig_color = sprite.color;
        switched = false;
    }

    // Update is called once per frame
    void Update() {
        if (getDistanceSqr() <= 9)
        {
            sprite.color = new Color(0.8f, 0.5f, 1f, 0.9f);
            if (Input.GetButtonDown("Use"))
            {
                switched = true;
            }
        }
        else
        {
            sprite.color = orig_color;
        }
    }

    public float getDistanceSqr() {
        Vector2 control_vec = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
        Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 controls_to_player = player_vec - control_vec;
        return controls_to_player.sqrMagnitude;
    }
}