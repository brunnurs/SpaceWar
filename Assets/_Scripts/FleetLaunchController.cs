using UnityEngine;
using System.Collections;

public class FleetLaunchController : MonoBehaviour {

	GameController gameController;

	GameObject sourcePlanet;
	GameObject targetPlanet;
	
	void Awake()
	{
		gameController = this.GetComponentInParent<GameController>();
	}


	void OnGUI()
	{
		if (Event.current.type == EventType.mouseDown) 
		{
			sourcePlanet = GetPlanetClickedOn ();

			TryEnableHalo (sourcePlanet);
		}

		if (Event.current.type == EventType.mouseUp) 
		{
			targetPlanet = GetPlanetClickedOn ();
			
			TryEnableHalo(targetPlanet);

			if (sourcePlanet != null && targetPlanet != null && sourcePlanet != targetPlanet)
			{
					Debug.Log("Start Fleet!");
			}
			else
			{
				ResetPlanets();
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

	void TryEnableHalo (GameObject planet)
	{
		if (planet != null) {
			Debug.Log ("Planet clicked!");
			var planetController = planet.GetComponentInChildren<PlanetController> ();
			planetController.EnableHalo ();
		}
	}

	void TryDisableHalo (GameObject planet)
	{
		if (planet != null) {
			var planetController = planet.GetComponentInChildren<PlanetController> ();
			planetController.DisableHalo();
        }
    }
    
	void ResetPlanets ()
	{
		TryDisableHalo (sourcePlanet);
		TryDisableHalo (targetPlanet);
		sourcePlanet = null;
		targetPlanet = null;
    }
}
