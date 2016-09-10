using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {

    public int health;
    public int speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(speed * transform.forward * Time.deltaTime);

	}
}
