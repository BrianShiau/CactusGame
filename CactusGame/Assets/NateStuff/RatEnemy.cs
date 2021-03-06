﻿using UnityEngine;
using System.Collections;

public class RatEnemy : AbstractEnemy {

    public RatEnemy()
    {

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        GameObject obj = col.gameObject;
		Debug.Log (obj.tag);
        if (obj.tag == "Player" && timeSinceLastAttack >= attackTimer)
        {
            obj.GetComponent<Player>().takeDamage(attack);
            timeSinceLastAttack = 0;
        }
        if (obj.tag == "Placeable" && timeSinceLastAttack >= attackTimer)
        {
            obj.GetComponent<Placeable>().takeDamage(attack);
            timeSinceLastAttack = 0;
        }
        if (obj.tag == "MamaCactus" && timeSinceLastAttack >= attackTimer)
        {
            obj.GetComponent<MamaCactus>().TakeDamage(attack);
            timeSinceLastAttack = 0;
        }

    }
}
