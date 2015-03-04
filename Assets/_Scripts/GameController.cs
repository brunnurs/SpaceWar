using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject planetPrototype;
	public int planetCount;
	public Vector3 borders;

	void Start () 
	{
		CreatePlanets ();
	}

	void CreatePlanets()
	{
		for (int i = 0; i < planetCount; i++) 
		{
			float xPosition = Random.Range (borders.x * -1, borders.x);
			float yPosition = Random.Range (borders.y * -1, borders.y);
			
			Vector3 positionVector = new Vector3 (xPosition, yPosition, -1);
			
			GameObject newPlanet =	Instantiate (planetPrototype, positionVector, Quaternion.identity) as GameObject;
		}
	}


}
