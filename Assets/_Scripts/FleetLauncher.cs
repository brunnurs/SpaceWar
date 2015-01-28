using UnityEngine;
using System.Collections;
using System.Linq;

public class FleetLauncher : MonoBehaviour 
{
	public GameObject spaceshipPrototype;

	private GameObject sourcePlanet;
	private GameObject targetPlanet;
	
	private GameController gameController;


	void Start()
	{
		gameController = this.GetComponentInParent<GameController>();
	}

	void OnGUI()
	{
		if (Event.current.type == EventType.MouseDown)
		{          
			sourcePlanet = GetPlanetUserClickedOn();
			TryEnableHalo (sourcePlanet);
		}
		
		if (Event.current.type == EventType.MouseUp)
		{          
			targetPlanet = GetPlanetUserClickedOn();
			TryEnableHalo(targetPlanet);

			if(sourcePlanet != null && targetPlanet != null && ClickedPlanetBelongsToCurrentPlayer())
			{
				LaunchFleet();
				StartCoroutine(DisableHalosInASecond());
			}
			else
			{
				ResetSelection ();
			}
		}
	}

	
	
	GameObject GetPlanetUserClickedOn ()
	{
		Camera currentCamera = Camera.main;
		
		Vector3 mouseposition = Input.mousePosition;
		Ray screenRay = currentCamera.ScreenPointToRay (mouseposition);
		
		RaycastHit rayhit;
		Physics.Raycast (screenRay, out rayhit);

		GameObject planetUserClicked = gameController.allPlanets.FirstOrDefault(p=> p.collider == rayhit.collider);

		return planetUserClicked;
	}

	bool ClickedPlanetBelongsToCurrentPlayer ()
	{
		if(!sourcePlanet.GetComponent<PlanetController>().CurrentOwner == gameController.currentPlayer)
		{
			Debug.Log(string.Format("Source planet does not belong to player {0}",gameController.currentPlayer));
			return false;
		}
		else
		{
			return true;
		}

	}

	void TryEnableHalo (GameObject planet)
	{
		if (planet != null) {
			planet.GetComponentInChildren<PlanetController> ().EnableHalo ();
		}
	}
	
	void TryDisableHalo (GameObject planet)
	{
		if (planet != null) {
			planet.GetComponentInChildren<PlanetController> ().DisableHalo ();
		}
	}	

	void ResetSelection ()
	{
		TryDisableHalo (sourcePlanet);
		sourcePlanet = null;

		TryDisableHalo (targetPlanet);
		targetPlanet = null;
	}

	IEnumerator DisableHalosInASecond()
	{
		yield return new WaitForSeconds(1);

		TryDisableHalo(sourcePlanet);
		TryDisableHalo(targetPlanet);

	}

	void LaunchFleet ()
	{
		PlanetController sourcePlanetController = sourcePlanet.GetComponent<PlanetController>();
		PlanetController targetPlanetController = targetPlanet.GetComponent<PlanetController>();

		SpaceshipController newShip = CreateSpaceship();

		Debug.Log(string.Format("Launch fleet from planet {0} to planet {1} with Fleetsize {2}",sourcePlanetController.planetNumber,targetPlanetController.planetNumber,newShip.fleetSize));
	}

	SpaceshipController CreateSpaceship ()
	{
		Vector3 targetDirection = targetPlanet.transform.position - sourcePlanet.transform.position;
		//with reducing the vector to length 1, we take care of the fact that the distance between two planets may vary
		targetDirection.Normalize();

		//The startposition of the spaceship needs to be next to the planet, in direction to the target, but with a little distance
		Vector3 spawnPosition = sourcePlanet.transform.position + targetDirection * 2f;

		//We rotate the ship to towards target planet when starting. With *270 degree, the ships surface looks towards the user.
		Quaternion rotation = Quaternion.LookRotation(targetDirection);
		rotation *= Quaternion.Euler(0,0,270);

		GameObject newSpaceship = Instantiate (spaceshipPrototype, spawnPosition, rotation) as GameObject;

		SpaceshipController spaceshipController = newSpaceship.GetComponent<SpaceshipController>();
		spaceshipController.targetPlanet = targetPlanet;
		spaceshipController.shipOwner = gameController.currentPlayer;

		CalculateFleetSizeAndShrinkPlanet (spaceshipController);

		return spaceshipController;
	}

	void CalculateFleetSizeAndShrinkPlanet (SpaceshipController spaceshipController)
	{
		GrowthController sourcePlanetGrowController = sourcePlanet.GetComponent<GrowthController> ();

		int shipsWeWillSend = (int)(sourcePlanetGrowController.ShipCounter * gameController.currentPlayer.fleetPercentage);

		sourcePlanetGrowController.ReduceShips (shipsWeWillSend);

		spaceshipController.fleetSize = shipsWeWillSend;
	}
}
