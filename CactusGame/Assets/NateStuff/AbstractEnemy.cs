using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {

    public int health;
    public int speed;
    public int attack;
    public float attackTimer;
    public int pointsWorth;

    public GameObject player;


    protected float timeSinceLastAttack;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(speed * transform.forward * Time.deltaTime);
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
        player.GetComponent<Player>().addScore(pointsWorth);
        Destroy(gameObject);
    }
}
