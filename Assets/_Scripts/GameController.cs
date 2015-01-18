using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour
{
    public float amountOfPlanets;
    public Vector3 spawnRange;
    public GameObject planetPrototype;

	private List<GameObject> allPlanets = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

        CreatePlanets ();
    }

	void CreatePlanets ()
	{
		for (int i = 0; i < amountOfPlanets; i++) 
		{
			SpawnPlanet ();
		}
	}

    void SpawnPlanet()
    {
        Vector3 spawnPosition = new Vector3 (Random.Range (-spawnRange.x, spawnRange.x), Random.Range (-spawnRange.y, spawnRange.y), 0);
		//while ())

        Quaternion spawnRotation = Quaternion.identity;
        GameObject newPlanet = Instantiate (planetPrototype, spawnPosition, spawnRotation) as GameObject;

        SetPlanetSizeRandomly(newPlanet);
    }

    void SetPlanetSizeRandomly(GameObject newPlanet)
    {
        GrowthController growthController = newPlanet.GetComponent<GrowthController>();
        growthController.ShipCounter = Mathf.Round(Random.Range(GrowthController.MIN_SHIPS, GrowthController.MAX_SHIPS));
    }
}
