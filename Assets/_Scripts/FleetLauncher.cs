using UnityEngine;
using System.Collections;
using System.Linq;

public class FleetLauncher : MonoBehaviour 
{
	public float fleetSizePercentage;

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

			if(sourcePlanet != null && targetPlanet != null)
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
		Debug.Log ("Reset planets");

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

		Debug.Log(string.Format("Launch fleet from planet {0} to planet {1}",sourcePlanetController.planetNumber,targetPlanetController.planetNumber));
	}
}
