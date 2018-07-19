using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReload : MonoBehaviour {

	public void Load()
	{
		SceneManager.LoadScene(0, LoadSceneMode.Single);	
	}
}
