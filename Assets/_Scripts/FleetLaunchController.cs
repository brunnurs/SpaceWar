using UnityEngine;
using System.Collections;

public class FleetLaunchController : MonoBehaviour {

	GameController gameController;

	GameObject sourcePlanet = null;
	GameObject targetPlanet = null;

	void Awake()
	{
		gameController = this.GetComponentInParent<GameController>();
	}


	void OnGUI()
	{

		if (Event.current.type == EventType.MouseDown) 
		{
			sourcePlanet = GetPlanetClickedOn();

			if(sourcePlanet != null)
			{
				Debug.Log("Hitted source Planet!");
				sourcePlanet.GetComponentInChildren<PlanetController>().EnableHalo();
			}
		}

		if (Event.current.type == EventType.mouseUp) 
		{
			targetPlanet = GetPlanetClickedOn();
			
			if(targetPlanet != null)
			{
				Debug.Log("Hitted target Planet!");
				targetPlanet.GetComponentInChildren<PlanetController>().EnableHalo();
			}

			if (sourcePlanet != null && targetPlanet != null) 
			{				
			}
			else
			{
				sourcePlanet.GetComponentInChildren<PlanetController>().DisableHalo();
				targetPlanet.GetComponentInChildren<PlanetController>().DisableHalo();

				sourcePlanet = null;
				targetPlanet = null;
			}
		}

	}

	GameObject GetPlanetClickedOn ()
	{
		Camera currentCamera = Camera.main;
		Vector3 currentCursorPosition = Input.mousePosition;

		Ray screenRay = currentCamera.ScreenPointToRay (currentCursorPosition);

		RaycastHit hit;
		Physics.Raycast (screenRay,out hit);

		foreach (GameObject planet in gameController.allPlanets) 
		{
			if(hit.collider == planet.GetComponent<Collider>())
			{
				return planet;
			}
		}

		return null;

	}
}
