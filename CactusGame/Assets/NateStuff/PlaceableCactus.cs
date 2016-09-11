using UnityEngine;
using System.Collections;

public class PlaceableCactus : Placeable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public PlaceableCactus()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        GameObject obj = col.gameObject;
        if (obj.tag == "Enemy" && timeSinceLastAttack >= attackTimer)
        {
            obj.GetComponent<AbstractEnemy>().takeDamage(attack);
            timeSinceLastAttack = 0;
        }

    }

}
