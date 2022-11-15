using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player components
    private Rigidbody2D playerRb2D;
    //player objects && variables
    private GameObject arrow;
    Quaternion arrowRotation;
    private GameObject pointer;
    //movement variables
    private float velX;
    private float velY; 
    private float velZ;
    public float horizontalInput;
    public float verticalInput;
    //health object && variables
    private GameObject healthObject;
    private GameObject healthUIMax;
    private GameObject healthUI;
    private float health;
    private float healthMax;
    //level objects && variables 
    private GameObject bar;
    private GameObject level;
    private float levelNum;
    private float changeRate;
    //misc
    public GameObject[] objectPrefabs;
    private Vector3 spawnLocation;
    private GameObject viewContainer;
    
    void Start()
    {
        viewContainer = GameObject.Find("ViewContainer");
        bar = GameObject.FindWithTag("Bar"); //find experience bar
        
        level = GameObject.FindWithTag("Level"); //find level number
        levelNum = int.Parse(level.GetComponent<UnityEngine.UI.Text>().text); //change level number from string to int
        
        changeRate = 10f; //set initial experience gain
    
        healthUI = GameObject.FindWithTag("HealthNumCurr"); //get current health string
        health = int.Parse(healthUI.GetComponent<UnityEngine.UI.Text>().text); //change current health string to int
        healthUIMax = GameObject.FindWithTag("HealthNumMax"); //get max health string
        healthMax = int.Parse(healthUIMax.GetComponent<UnityEngine.UI.Text>().text); //change max health string to int
        healthObject = GameObject.FindWithTag("Health"); //get health bar object

        arrow = GameObject.Find("SpawnArrow"); //get arrow Object
        pointer = GameObject.Find("Pointer"); //get pointer object
        playerRb2D = GetComponent<Rigidbody2D>(); //get players rigid body 
    }

    void Update()
    {
        //movement
        if(Input.GetKey(KeyCode.D)){
            velX = -2;
        } else if(velX < 0 && !Input.GetKey(KeyCode.D)){
            velX = 0;
        }
        if(Input.GetKey(KeyCode.A)){
            velX = 2;
        } else if(velX > 0 && !Input.GetKey(KeyCode.A)){
            velX = 0;
        }
        if(Input.GetKey(KeyCode.W)){
            velY = 2;
        } else if(velY > 0 && !Input.GetKey(KeyCode.W)){
            velY = 0;
        }
        if(Input.GetKey(KeyCode.S)){
            velY = -2;
        } else if(velY < 0 && !Input.GetKey(KeyCode.S)){
            velY = 0;
        }
        playerRb2D.velocity = new Vector3(velX,velY,velZ);
        
        //spawn bullets
        if (Input.GetMouseButtonDown(0)){
            spawnLocation = new Vector3(arrow.transform.position.x,arrow.transform.position.y,1f);
            arrowRotation = arrow.transform.rotation;
            SpawnBullet();
        }

        //level up
        health = int.Parse(healthUI.GetComponent<UnityEngine.UI.Text>().text);
        if(bar.transform.localScale.x <= -20.8f){ //Past level Up threshold 
            //reset bar location
            bar.transform.localScale = new Vector3 (-1f, 0.24f, 0f);
            bar.transform.localPosition = new Vector3(8.2f, 1.629842f, 1f);
            levelNum++;
            //add health
            healthMax += 50;
            health += 50;
            healthUIMax.GetComponent<UnityEngine.UI.Text>().text = $"{healthMax}";
            healthUI.GetComponent<UnityEngine.UI.Text>().text = $"{health}";
            //set new change rate
            changeRate = changeRate/levelNum;
        }
        level.GetComponent<UnityEngine.UI.Text>().text = $"{levelNum}";

        //update health
        health = int.Parse(healthUI.GetComponent<UnityEngine.UI.Text>().text);
        healthMax = int.Parse(healthUIMax.GetComponent<UnityEngine.UI.Text>().text);
        if(health < healthMax){
            healthObject.transform.localPosition = new Vector3((-1 * (health/ healthMax)) / 2, 0, 1f);
            healthObject.transform.localScale = new Vector3(1 * (health / healthMax), 0.1f, 1f);
        }
        
    }
    void OnTriggerStay2D(Collider2D col)
    {
        //lose health
        if (col.gameObject.tag == "Enemy") {
            health--;
            healthUI.GetComponent<UnityEngine.UI.Text>().text = $"{health}";
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {   
        //gain experience        
        if (col.gameObject.tag == "Experience")
        {
            bar.transform.localScale -= new Vector3(changeRate, 0f, 0f);
            bar.transform.Translate(new Vector3(-changeRate/2, 0f, 0f));
            Destroy(col.gameObject);
        } 
        //if(FindObjectsOfType<Enemy>().Length != 0){ }
            if (col.gameObject.tag == "LeftDoor")
                {
                    Destroy(GameObject.FindWithTag("Enemy"));
                    Vector3 storePos = transform.position;
                    viewContainer.transform.Translate(new Vector3(36.5f,0,0));
                    transform.position = new Vector3(storePos.x + 3.1f, storePos.y, storePos.z);
                }
                if (col.gameObject.tag == "RightDoor")
                {
                    Vector3 storePos = transform.position;
                    viewContainer.transform.Translate(new Vector3(-36.5f,0,0));
                    transform.position = new Vector3(storePos.x + -3.1f, storePos.y, storePos.z);
                }
        
        
    }

    //bullet spawning method
    void SpawnBullet ()
    {
            Instantiate(objectPrefabs[0], spawnLocation, arrowRotation);
    }
}
