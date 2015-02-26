﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour
{
    public float amountOfPlanets;
    public Vector3 spawnRange;
    public GameObject planetPrototype;

	public PlayerController currentPlayer;

	public List<GameObject> allPlanets = new List<GameObject>();
	
    void Start()
    {
        CreatePlanets ();
		SetPlayerOnRandomPlanet();
    }

	void SetPlayerOnRandomPlanet ()
	{
		int startPlanetIndex = Random.Range(0,allPlanets.Count);
		allPlanets[startPlanetIndex].GetComponent<PlanetController>().CurrentOwner = currentPlayer;
	}

	void CreatePlanets ()
	{
		for (int i = 0; i < amountOfPlanets; i++) 
		{
			SpawnPlanet (i);
		}
	}

    void SpawnPlanet(int planetNumber)
    {
        Vector3 spawnPosition = new Vector3 (Random.Range (-spawnRange.x, spawnRange.x), Random.Range (-spawnRange.y, spawnRange.y), -2);

		while(IsSpawnPositionTooCloseToOtherPlanets(spawnPosition))
		{
			Debug.Log("two planets are too close! We have to retry");
			spawnPosition = new Vector3 (Random.Range (-spawnRange.x, spawnRange.x), Random.Range (-spawnRange.y, spawnRange.y), -2);
		}
				

        Quaternion spawnRotation = Quaternion.identity;
        GameObject newPlanet = Instantiate (planetPrototype, spawnPosition, spawnRotation) as GameObject;

		SetPlanetNumber(newPlanet,planetNumber);
        SetPlanetSizeRandomly(newPlanet);
		SetPlanetShipsRandomly(newPlanet);
		allPlanets.Add(newPlanet);
    }

    void SetPlanetSizeRandomly(GameObject newPlanet)
    {
        float diameter = Random.Range(GrowthController.MIN_SIZE, GrowthController.MAX_SIZE);
		newPlanet.transform.localScale = new Vector3(diameter,diameter,diameter);
    }

	void SetPlanetShipsRandomly (GameObject newPlanet)
	{
		GrowthController growthController = newPlanet.GetComponent<GrowthController>();
		growthController.ShipCounter = Random.Range(GrowthController.MIN_SHIPS, GrowthController.MAX_SHIPS);
//		growthController.ShipCounter = 10;
	}

	void SetPlanetNumber (GameObject newPlanet,int planetNumber)
	{
		PlanetController planetController = newPlanet.GetComponent<PlanetController>();
		planetController.planetNumber = planetNumber;
	}

	bool IsSpawnPositionTooCloseToOtherPlanets (Vector3 spawnPosition)
	{
		foreach(GameObject planet in allPlanets)
		{
			float distance = Vector3.Distance(spawnPosition,planet.transform.localPosition);

			if(distance < GrowthController.MAX_SIZE)
			{
				return true;
			}
		}

		return false;
	}
}
