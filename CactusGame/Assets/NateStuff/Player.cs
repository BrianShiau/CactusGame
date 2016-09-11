using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public int health;
    public int water;
    public float respawnTimer;
    
    public List<Placeable> inventory;
    public GameObject world; 

    private int selectedItem;
    private Vector3 respawnLocation;
    private float timeSinceDeath;
    private int startingHealth;

    // Use this for initialization
    void Start () {
        respawnLocation = transform.position;
        startingHealth = health;

	}
	
	void FixedUpdate () {

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

        }
         
    }

    public Placeable getSelectedItem()
    {
        return inventory[selectedItem];
    }

    public void takeDamage(int damage)
    {
        health-=damage;
        if (health <= 0)
            kill();
    }

    public void kill()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = respawnLocation; 
        
    }

    public void loseWater(int lost)
    {
        water -= lost;
    }

    public void addWater(int added)
    {
        water += added;
    }


}
