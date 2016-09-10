using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public int speed;
    public int health;
    public int water;
    public List<Placeable> inventory;

    private int selectedItem;


	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate () {
        Vector3 movementVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
            movementVector.z++;
        if (Input.GetKey(KeyCode.A))
            movementVector.x--;
        if (Input.GetKey(KeyCode.S))
            movementVector.z--;
        if (Input.GetKey(KeyCode.D))
            movementVector.x++;

        movementVector.Normalize();
        Vector3 newPos = movementVector * speed * Time.deltaTime;
        transform.Translate(newPos);


        if (Input.GetKey(KeyCode.E))
        {
            // check item cost
            if(inventory[selectedItem].cost <= water)

        }

            

        
    }

}
