using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteCreation : MonoBehaviour {

	public float StartHeight = 15; 
	public Vector2 Arena = new Vector2(15,15); 
	public GameObject Meteorite; 
	float timer = 15; 


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime; 
		if(timer <= 0)
		{
			Vector3 pos = new Vector3(Random.Range(-Arena.x/2f, Arena.x/2f), StartHeight, Random.Range(-Arena.x/2f, Arena.x/2f)); 
			GameObject g = Instantiate(Meteorite, pos, Quaternion.identity) as GameObject; 
			timer = Random.Range(8f,15f); 
		}

		
	}
}
