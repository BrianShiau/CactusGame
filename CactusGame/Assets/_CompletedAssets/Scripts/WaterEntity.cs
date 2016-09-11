using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class WaterEntity : MonoBehaviour {


    public int water;
	// Use this for initialization
	void Start () {
        water = Random.Range(25,50);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().addWater(water);
            Destroy(this.gameObject);
        }
    }
}
