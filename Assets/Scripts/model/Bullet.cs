using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Clyde")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Blinky")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Inky")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Pinky")
        {
            Destroy(gameObject);
        }
    }
}
