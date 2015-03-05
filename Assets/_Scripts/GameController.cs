using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : Photon.MonoBehaviour
{
	private const string GAME_VERSION = "1.0";
	private const string DEFAULT_ROOM = "DefaultRoom";
	private const string PLANET_PREFAB_NAME = "Planet";

    public float amountOfPlanets;
    public Vector3 spawnRange;

	public PlayerController currentPlayer;

	public List<GameObject> allPlanets = new List<GameObject>();



	public void JoinRoom()
	{
		PhotonNetwork.JoinRoom (DEFAULT_ROOM);
        Debug.Log ("Join Room " + DEFAULT_ROOM);
	}

	public void CreateRoom()
	{
		PhotonNetwork.CreateRoom (DEFAULT_ROOM);
		Debug.Log ("Create Room " + DEFAULT_ROOM);
    }

    public void StartGame()
    {
		Debug.Log ("Start Game");
		photonView.RPC ("GameStarted", PhotonTargets.AllBufferedViaServer, null);

        CreatePlanets ();
		SetPlayerOnRandomPlanet();
    }

	[RPC]
	public void GameStarted()
	{
		this.GetComponentInChildren<Canvas> ().gameObject.SetActive (false);
		Debug.Log ("Game started!");
	}


	void Start()
	{
		PhotonNetwork.ConnectUsingSettings (GAME_VERSION);
		Debug.Log ("Connected to PhotonNetwork");
	}

	void SetPlayerOnRandomPlanet ()
	{
		int startPlanetIndex = Random.Range(0,allPlanets.Count);
		GameObject choosenPlanet = allPlanets [startPlanetIndex];

		choosenPlanet.GetComponent<PlanetController>().CurrentOwner = currentPlayer;

		IncreaseSizeWhenPlayerPlanetTooShort (choosenPlanet);
	}

	void IncreaseSizeWhenPlayerPlanetTooShort(GameObject choosenPlanet)
	{
		if (choosenPlanet.GetComponent<GrowthController> ().ShipCounter < GrowthController.MAX_SHIPS / 2) 
		{
			choosenPlanet.GetComponent<GrowthController> ().ShipCounter = Random.Range(GrowthController.MAX_SHIPS / 2,GrowthController.MAX_SHIPS);
		}
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
        GameObject newPlanet = PhotonNetwork.Instantiate(PLANET_PREFAB_NAME, spawnPosition, spawnRotation,0) as GameObject;

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
