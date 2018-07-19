using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamSimple : MonoBehaviour {


	[Header("\t Basics")]
	public bool UseMouse = false; 
	public Transform Target; 
	public float DampSpeed = 2;
	public float LookFocusSpeed = 5; 
	public float RotationSpeed = 5; 

	[Header("\t Thresholds")]
	public Vector2 MouseSensibility;  
	public Vector2 JoystickSensibility;
	public Vector2 YLimits; 

	Vector3 Offset; 
	Vector3 velocity= Vector3.zero; 

	float angleY = 0; 
	float angleX = 0; 

	Vector3 LookTarget; 
	private Vector3 vel = Vector3.zero; 

	// Use this for initialization
	void Start () {
	
		Offset = transform.position - Target.position; 
		LookTarget = Target.position; 		

	}
	
	// Update is called once per frame
	void Update () {

		float ix,iy; 
		Vector2 Sensibility; 
		if(UseMouse)
		{
			ix = Input.GetAxis("mHorizontal");
			iy = Input.GetAxis("mVertical");
			Sensibility = MouseSensibility;
		}
		else
		{
			ix = Input.GetAxis("HorCam"); 
			iy = Input.GetAxis("VerCam"); 
			Sensibility = JoystickSensibility; 
		}

		// Debug.Log(ix*MouseSensibility.x); 

		if(Target != null)
		{
			AdaptPosition(ix, iy, Sensibility); 
			AdjustTarget(); 
		}

	}

	void AdaptPosition(float x, float y, Vector2 s)
	{
		// Vector3 TargetPos = Target.position + Rotate(x*s.x, y*s.y)*Offset; 
		// transform.position = Vector3.SmoothDamp(transform.position, TargetPos,ref vel, DampSpeed); 
		Vector3 RotatedOffset = Rotate(x*s.x, y*s.y)*Offset; 
		transform.position = Vector3.SmoothDamp(transform.position, Target.position + RotatedOffset,ref vel, DampSpeed); 
		// transform.LookAt(LookTarget);
	}

	void AdjustTarget()
	{
		LookTarget = Target.position; 
	}

	void LateUpdate()
	{
		
		transform.LookAt(LookTarget); 		
	}
	void ResetAngles()
	{
		angleY = 0; 
		angleX = 0; 
	}

	Quaternion Rotate(float x, float y)
	{
		angleX += x;
		angleY += y; 

		angleY = Mathf.Clamp(angleY,YLimits.x, YLimits.y);
		// Quaternion r = Quaternion.AngleAxis(angleX, Target.right)*Quaternion.AngleAxis(angleY,Target.up); 
		Quaternion r = Quaternion.Euler(angleY, angleX,0); 
		 // Quaternion Euler(float x, float y, float z); 
		return r; 
	}
}



