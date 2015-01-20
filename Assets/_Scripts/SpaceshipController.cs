using UnityEngine;
using System.Collections;

public class SpaceshipController : MonoBehaviour 
{
	public GameObject targetPlanet;
	public int firepower;
	public float speed;

	// Use this for initialization
	void Start () 
	{
		Debug.Log("12");
		Vector3 targetDirection = targetPlanet.transform.position - this.transform.position;
		rigidbody.velocity = targetDirection * speed;
	}



}
