using UnityEngine;
using System.Collections;

public class MamaCactus : MonoBehaviour {

	public Player player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(int damage){
		player.loseWater (damage);
	}
}
