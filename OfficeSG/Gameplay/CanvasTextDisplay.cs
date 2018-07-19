using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class CanvasTextDisplay : MonoBehaviour {

	public TextMeshProUGUI text; 
	MonsterCreation mc; 

	// Use this for initialization
	void Start () {

		mc = GetComponent<MonsterCreation>(); 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeText(string s)
	{
		text.text = "Zombies " + s; 
	}
}
