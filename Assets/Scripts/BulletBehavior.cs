using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRb2D;
    private Rigidbody2D bulletRb2D;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRb2D = player.GetComponent<Rigidbody2D>();
        bulletRb2D = GetComponent<Rigidbody2D>();
        bulletRb2D.AddForce(transform.up * 250f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall")
            {
                Destroy(gameObject);
            }
    }
}
