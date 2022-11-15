using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour
{
    private GameObject player;
    private Vector3 pLocation;
    public GameObject experience;
    private GameObject health;
    private GameObject healthUI;
    private float healthNum;
    private float HchangeRate;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        pLocation = player.transform.position;

        if(!(transform.position == pLocation)){
            if(transform.position != pLocation){
                 transform.position = Vector3.MoveTowards(transform.position, new Vector3(pLocation.x,pLocation.y,1), Time.deltaTime * 1);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet")
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
                Instantiate(experience, transform.position, experience.transform.rotation);
            }
    }
   
}
