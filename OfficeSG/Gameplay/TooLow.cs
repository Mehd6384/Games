using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooLow : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.GetComponent<SeparationChair>())
		{
			GameObject G =other.gameObject;
			G.GetComponent<SeparationChair>().LaunchSeparation(); 
			Destroy(G, 5f); 
			Camera.main.GetComponent<TPSCamSimple>().Target = null; 
			Destroy(G.GetComponent<SeparationChair>().Chair, 4f); 
		}
	}
}
