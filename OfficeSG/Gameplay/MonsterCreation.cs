using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreation : MonoBehaviour {

	public GameObject Zombie; 
	public GameObject Player; 
	public Vector2 Arena; 
	public int NbMonster; 
	public LayerMask Terrain; 
	public float IniHeight; 

	public List<Transform> ZombieGates = new List<Transform>(); 

	[HideInInspector]
	public int DeadMonster = 0; 
	int DeadSinceStart = 0; 

	LaunchScene scene_managment; 
	List<GameObject> zombies = new List<GameObject>(); 
	CanvasTextDisplay canvas; 



	// Use this for initialization
	void Start () {

		canvas = GetComponent<CanvasTextDisplay>(); 
		InitiateCanvas(); 
		scene_managment = GetComponent<LaunchScene>() ;
		Populate(); 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void Populate()
	{
		int created = 0; 
		while (created < NbMonster)
		{
			TestedPosition p = TestZombieGatePosition(); 
			if(p.valid)
			{
				Vector3 pos = new Vector3(p.pos.x, IniHeight, p.pos.z); 
				created += 1; 
				GameObject G = Instantiate(Zombie,pos, Zombie.transform.rotation) as GameObject; 
				G.GetComponent<ZombieLogic>().Target = Player.transform; 
				G.GetComponent<ZombieLogic>().mothership = gameObject; 

				zombies.Add(G); 
			}
		}
	}

	TestedPosition TestZombieGatePosition()
	{
		int GateNb = Random.Range(0, ZombieGates.Count); 
		Vector3 decal = new Vector3(Random.Range(-1f,1f), 0,Random.Range(-1f,1f)); 

		Ray ray = new Ray(ZombieGates[GateNb].position + decal, Vector3.down); 
		RaycastHit hit; 

		if(Physics.Raycast(ray, out hit, 20, Terrain))
		{
			return new TestedPosition(ray.origin, true); 
		}

		return new TestedPosition();

	}

	TestedPosition TestRandomPosition()
	{

		float x = Random.Range(-Arena.x/2,Arena.x/2);
		float y = Random.Range(-Arena.y/2,Arena.y/2); 

		Ray ray = new Ray(transform.position + new Vector3(x,5f, y), Vector3.down); 
		RaycastHit hit; 

		if(Physics.Raycast(ray, out hit, 20, Terrain))
		{
			return new TestedPosition(ray.origin, true); 
		}

		return new TestedPosition();

	}

	public void AddDeathToCounter()
	{
		DeadSinceStart += 1; 
		DeadMonster += 1; 
		// canvas.ChangeText(DeadMonster.ToString() + "/" + NbMonster.ToString()); 
		canvas.ChangeText(DeadSinceStart.ToString());

		if(DeadMonster >= NbMonster - Random.Range(2, NbMonster/2)) 
		{
			NbMonster = NbMonster + Random.Range(1,5); 
			NbMonster = (NbMonster > 20) ? 20 : NbMonster; 
			DeadMonster = 0; 
			Populate(); 
		}
		// if(DeadMonster == NbMonster)
		// {
		// 	FinishGame(); 
		// 	Player.GetComponent<OfficeSGControllerV2>().enabled = false; 
		// }
	}

	public void FellDuringCombat()
	{
		FinishGame(); 
		Debug.Log("You lost"); 
		LoadNextScene(); 
	}

	void FinishGame()
	{
		zombies.Clear(); 
		LoadNextScene(); 
	}
	void LoadNextScene()
	{
		scene_managment.Load(); 
	}

	void InitiateCanvas()
	{
		canvas.ChangeText(DeadMonster.ToString()); 
	}
}

public class TestedPosition
{
	public Vector3 pos;
	public bool valid; 

	public TestedPosition()
	{
		valid = false; 
		pos = Vector3.zero;
	}

	public TestedPosition(Vector3 v, bool b)
	{
		pos = v; 
		valid = b; 
	}
}


