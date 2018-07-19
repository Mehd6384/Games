using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitbox : MonoBehaviour {
	

	public float delay = -0.10f; 

	public void SetDelay(float d)
	{
		delay = d; 
	}

	void Update()
	{
		delay -= Time.deltaTime; 
	}

	void OnTriggerEnter(Collider other)
	{
		if(delay <= 0f)
		{
			SeparationChair sp = other.transform.root.gameObject.GetComponent<SeparationChair>();
			if(sp != null)
			{
				sp.LaunchSeparation(); 
			}
		}
	}
}
