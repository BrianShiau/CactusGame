using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	public GameObject[][] grid;

	// Use this for initialization
	void Start () {
		grid = new GameObject[8][];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){

	}

	public bool AddToWorld(float x, float y, GameObject plant){
		int angle = (int) (WorldToAngle (x, y) / 45);
		int magnitude = (int) (WorldToMagnitude (x, y) / 20);
		if (grid [angle] [magnitude])
			return false;
		grid[angle][magnitude] = plant;

		float newAngle = angle * 45f + 22.5f;
		float newMagnitude = magnitude * 20f + 10f;
		float newX = newMagnitude * Mathf.Cos(newAngle);
		float newY = newMagnitude * Mathf.Sin(newAngle);
		plant.transform.position.Set (newX, newY, 0f);
		return true;
	}

	public float WorldToAngle(float x, float y){
		return Mathf.Atan2 (y, x) * (180 / Mathf.PI);
	}

	public float WorldToMagnitude(float x, float y){
		return Mathf.Sqrt (Mathf.Pow (x, 2) + Mathf.Pow (y, 2));
	}
}
