using UnityEngine;
using System.Collections;

public class WaterEntityHandler : MonoBehaviour {

    public GameObject waterEntity;
    int count;
    int delay = 1000;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", 5, 5);
    }
	
	// Update is called once per frame
	void Update () {

	}

    void Spawn()
    {
        Instantiate(waterEntity);
    }
}
