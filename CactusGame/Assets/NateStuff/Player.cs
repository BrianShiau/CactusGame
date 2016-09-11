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
    public float respawnTimer;
    public float attackTimer;
    public float placementTimer;
    public float attackDistance;
    public GameObject world;
    public GameObject UICanvas;

    // add one of these for each placeable
    public Transform placeableCactus;

    private int selectedItem;
    private Vector3 respawnLocation;
    private float timeSinceDeath;
    private float timeSinceLastAttack;
    private float timeSinceLastPlacement;
    private int startingHealth;
    public Slider healthSlider;
    public Slider waterSlider;
	public Image healthSliderFill;
	public Image waterSliderFill;
    private Text scoreText;
    private List<Placeable> inventory;
    private List<Transform> uninstancedPlaceables;


    // Use this for initialization
    void Start () {
        respawnLocation = transform.position;
        startingHealth = health;
        Slider[] UISliders = UICanvas.GetComponentsInChildren<Slider>();
        scoreText = UICanvas.GetComponentInChildren<Text>();
        scoreText.text = "Score: 0";
        healthSlider = UISliders[0];
        waterSlider = UISliders[1];
        healthSlider.maxValue = health;
        waterSlider.maxValue = waterCap;
        waterSlider.value = water;
		healthSliderFill.color = Color.red;
		waterSliderFill.color = Color.blue;

        // ---need to do this for each new placeable object
        uninstancedPlaceables.Add(placeableCactus);
        GameObject placeableObj = (GameObject) Instantiate(placeableCactus, transform.position, Quaternion.identity);
        placeableObj.SetActive(false);
        inventory.Add(placeableObj.GetComponent<Placeable>());
        // ---


	}
	
	void FixedUpdate () {

        if (timeSinceDeath >= respawnTimer)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            health = startingHealth;
        }

        if (health > 0)
        {


            if (Input.GetKey(KeyCode.E))
            {
                // check item cost
                if (inventory[selectedItem].cost <= water && timeSinceLastPlacement >= placementTimer)
                {
                    GameObject placeableObj = (GameObject)Instantiate(uninstancedPlaceables[selectedItem], transform.position, Quaternion.identity);
                    world.GetComponent<Grid>().AddToWorld(transform.position.x, transform.position.z, placeableObj);
                    water -= inventory[selectedItem].cost;
                    timeSinceLastPlacement = 0;
                }
            }

            if (Input.GetKey(KeyCode.Q))
            {
                selectedItem++;
                if (selectedItem >= inventory.Count)
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

    public Placeable getSelectedItem()
    {
        return inventory[selectedItem];
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
