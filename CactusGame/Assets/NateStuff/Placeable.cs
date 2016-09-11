using UnityEngine;
using System.Collections;

public abstract class Placeable : MonoBehaviour {

    public int cost;
    public int health;
    public int attack;
    public float attackTimer;

    protected float timeSinceLastAttack;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastAttack += Time.deltaTime;
	}

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            kill();
    }

    public void kill()
    {
        Destroy(gameObject);
    }
}
