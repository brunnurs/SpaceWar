using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject planetPrototype;
	public int planetCount;
	public Vector3 borders;

	public List<GameObject> allPlanets;

	void Start () 
	{
		allPlanets = new List<GameObject> ();

		CreatePlanets ();
	}

	void CreatePlanets()
	{
		for (int i = 0; i < planetCount; i++) 
		{
			float xPosition = Random.Range (borders.x * -1, borders.x);
			float yPosition = Random.Range (borders.y * -1, borders.y);
			
			Vector3 positionVector = new Vector3 (xPosition, yPosition, -1);


			while (IsTooCloseToOtherPlanets(positionVector))
			{
				xPosition = Random.Range (borders.x * -1, borders.x);
				yPosition = Random.Range (borders.y * -1, borders.y);

				positionVector = new Vector3 (xPosition, yPosition, -1);
			}
			
			GameObject newPlanet =	Instantiate (planetPrototype, positionVector, Quaternion.identity) as GameObject;
			SetRandomPlanetSize(newPlanet);
			SetRandomShipCounter(newPlanet);

			allPlanets.Add(newPlanet);
		}
	}

	void SetRandomPlanetSize (GameObject newPlanet)
	{
		float newDiameter = Random.Range (GrowthController1.MIN_SIZE, GrowthController1.MAX_SIZE);
		newPlanet.transform.localScale = new Vector3(newDiameter,newDiameter,newDiameter);
	}

	void SetRandomShipCounter (GameObject newPlanet)
	{
		GrowthController1 growthController = newPlanet.GetComponentInChildren<GrowthController1> ();
		growthController.SpaceShipCounter = Random.Range (GrowthController1.MIN_SHIPCOUNTER, GrowthController1.MAX_SHIPCOUNTER);

	}

	bool IsTooCloseToOtherPlanets (Vector3 randomPosition)
	{
		foreach (GameObject planet in allPlanets) 
		{
			if(Vector3.Distance(randomPosition,planet.transform.position) < GrowthController1.MAX_SIZE)
			{
				return true;
			}
		}

		return false;
	}
}
