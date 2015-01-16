using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public float amountOfPlanets;
    public Vector3 spawnRange;
    public GameObject planet;


    // Use this for initialization
    void Start()
    {
        Vector3 spawnPosition = new Vector3 (Random.Range (-spawnRange.x, spawnRange.x), Random.Range (-spawnRange.y, spawnRange.y), 0);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate (planet, spawnPosition, spawnRotation);
    }

    void SpawnPlanet()
    {
        Vector3 spawnPosition = new Vector3 (Random.Range (-spawnRange.x, spawnRange.x), Random.Range (-spawnRange.y, spawnRange.y), 0);
        Quaternion spawnRotation = Quaternion.identity;
        GameObject newPlanet = Instantiate (planet, spawnPosition, spawnRotation) as GameObject;

        SetPlanetSizeRandomly();
        GrowthController growthController = newPlanet.GetComponent<GrowthController>();
        growthController.ShipCounter = Random.Range(GrowthController.MIN_SHIPS, GrowthController.MAX_SHIPS);
    }

    
    // Update is called once per frame
    void Update()
    {
    
    }
}
