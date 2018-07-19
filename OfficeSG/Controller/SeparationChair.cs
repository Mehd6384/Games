using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparationChair : MonoBehaviour {

	public GameObject Chair;
	// public bool Separate; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

		// if(Separate)
		// {
		// 	Separate = false; 
		// 	LaunchSeparation();
		// }

	}

	public void LaunchSeparation()
	{
		Chair.transform.parent = null; 
		Chair.AddComponent<Rigidbody>(); 
		Chair.AddComponent<BoxCollider>(); 
		Chair.GetComponent<Rigidbody>().AddExplosionForce(20, transform.position-Vector3.up, 10, 5f);
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		GetComponent<Rigidbody>().AddExplosionForce(1, Chair.transform.position-Vector3.up, 10, 5f);

		Camera.main.GetComponent<MonsterCreation>().FellDuringCombat(); 
	}
}
