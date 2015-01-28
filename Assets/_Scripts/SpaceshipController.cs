using UnityEngine;
using System.Collections;

public class SpaceshipController : MonoBehaviour 
{
	public PlayerController shipOwner;
	public GameObject targetPlanet;
	public int fleetSize;
	public float speed;

	// Use this for initialization
	void Start () 
	{
		Vector3 targetDirection = targetPlanet.transform.position - this.transform.position;
		//Also here: every ship should have the same speed, so the distance to the target should be irrelevant.
		targetDirection.Normalize();

		rigidbody.velocity = targetDirection * speed;
	}



}
