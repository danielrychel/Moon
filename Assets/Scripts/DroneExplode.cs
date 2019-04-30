using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneExplode : MonoBehaviour
{
    // Start is called before the first frame update

    public int dmg;
    public Transform explosion;

    public AudioClip explosionSound;

    private bool destroy;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            destroy = true;
            SoundManager.instance.PlaySingle(explosionSound);
            SelfDestruct(collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            destroy = false;
        }
    }

    private void SelfDestruct(Collider2D collision)
    {
        transform.parent.transform.Rotate(new Vector3(90, 0, 0), Space.World);
        transform.parent.transform.Rotate(new Vector3(40, 0, 0), Space.World);
        transform.parent.transform.Rotate(new Vector3(20, 0, 0), Space.World);

        Instantiate(explosion, transform.position, Quaternion.identity);

        if (!destroy) return;

        collision.gameObject.GetComponent<Health>().takeDamage(dmg);
        Debug.Log("Splosion!");
        // Explosion.exe;
        Destroy(transform.parent.gameObject);
    }
}
