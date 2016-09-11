using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour {
    public int health;
    public int water;
    public int waterCap;
    public int attackDamage;
    public int score;
    public int numPlaceables;
    public float respawnTimer;
    public float attackTimer;
    public float placementTimer;
    public float attackDistance;
    public GameObject world;
    public GameObject UICanvas;

    // add one of these for each placeable
    public GameObject placeableCactus;

    private int selectedItem;
    private Vector3 respawnLocation;
    private float timeSinceDeath;
	private bool dead;
    private float timeSinceLastAttack;
    public float timeSinceLastPlacement;
    private int startingHealth;
    public Slider healthSlider;
    public Slider waterSlider;
	public Image healthSliderFill;
	public Image waterSliderFill;
    private Text scoreText;


    // Use this for initialization
    void Start () {
        respawnLocation = transform.position;
        startingHealth = health;
        Slider[] UISliders = UICanvas.GetComponentsInChildren<Slider>();
        scoreText = UICanvas.GetComponentInChildren<Text>();
        scoreText.text = "Score: 0";
        //healthSlider = UISliders[0];
        //waterSlider = UISliders[1];
        healthSlider.maxValue = health;
        waterSlider.maxValue = waterCap;
        waterSlider.value = water;
		healthSliderFill.color = Color.red;
		waterSliderFill.color = Color.blue;


	}
	
	void FixedUpdate () {

        if (dead && timeSinceDeath >= respawnTimer)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            health = startingHealth;
			healthSlider.value = health > 0 ? health : 0;
			dead = false;
        }

        if (health > 0)
        {


            if (Input.GetKey(KeyCode.E))
            {
                // check item cost
                if (10 <= water && timeSinceLastPlacement >= placementTimer)
                {
                    water -= 10;
                    timeSinceLastPlacement = 0.0f;
                    GameObject placeableObj = (GameObject)Instantiate(placeableCactus, transform.position, Quaternion.identity);
                    world.GetComponent<Grid>().AddToWorld(transform.position.x, transform.position.z, placeableObj);
                    
                }
            }

            if (Input.GetKey(KeyCode.Q))
            {
                selectedItem++;
                if (selectedItem >= numPlaceables)
                    selectedItem = 0;

            }
        }
        else
        {
            // dead, wait for respawn
        }

        timeSinceLastAttack += Time.deltaTime;
        timeSinceDeath += Time.deltaTime;
        timeSinceLastPlacement += Time.deltaTime;

    }

    public GameObject getSelectedItem()
    {
        return placeableCactus;
    }

    public void takeDamage(int damage)
    {
        health-=damage;
        healthSlider.value = health > 0 ? health : 0;
        if (health <= 0)
            kill();
    }

    public void kill()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.position = respawnLocation;
        timeSinceDeath = 0;
		dead = true;
    }

    public void addWater(int added)
    {
        if (water + added <= waterCap)
            water += added;
        else
            water = waterCap;

        waterSlider.value = water;

    }

    public void addScore(int score)
    {
        this.score += score;
        scoreText.text = "Score: " + score;

    }



	public void loseWater(int loss){
		water -= loss;
        waterSlider.value = water > 0 ? water : 0;
		if (water <= 0){
			//lose the game
		}
	}

    public void attack()
    {
        if (timeSinceLastAttack >= attackTimer)
        {
            RaycastHit hitinfo;
            Physics.Raycast(transform.position, transform.forward, out hitinfo, attackDistance);
            if (hitinfo.collider != null)
            {
                if (hitinfo.collider.tag == "Enemy")
                {
                    hitinfo.collider.gameObject.GetComponent<AbstractEnemy>().takeDamage(attackDamage);
                }
            }

            timeSinceLastAttack = 0;
        }
    }

}
