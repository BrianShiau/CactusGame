using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public int health;
    public int water;
    public int waterCap;
    public int attackDamage;
    public float respawnTimer;
    public float attackTimer;
    public float attackDistance;

    
    public List<Placeable> inventory;
    public GameObject world;
    public GameObject UICanvas;

    private int selectedItem;
    private Vector3 respawnLocation;
    private float timeSinceDeath;
    private float timeSinceLastAttack;
    private int startingHealth;
    public Slider healthSlider;
    public Slider waterSlider;
	public Image healthSliderFill;
	public Image waterSliderFill;
    

    // Use this for initialization
    void Start () {
        respawnLocation = transform.position;
        startingHealth = health;
        Slider[] UISliders = UICanvas.GetComponentsInChildren<Slider>();
        healthSlider = UISliders[0];
        waterSlider = UISliders[1];
        healthSlider.maxValue = health;
        waterSlider.maxValue = waterCap;
		healthSliderFill.color = Color.red;
		waterSliderFill.color = Color.blue;

	}
	
	void FixedUpdate () {

        if (timeSinceDeath >= respawnTimer)
            health = startingHealth;

        if (health > 0)
        {


            if (Input.GetKey(KeyCode.E))
            {
                // check item cost
                if (inventory[selectedItem].cost <= water)
                {
                    world.GetComponent<Grid>().AddToWorld(transform.position.x, transform.position.z, inventory[selectedItem].gameObject);
                    water -= inventory[selectedItem].cost;
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
        gameObject.SetActive(false);
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
