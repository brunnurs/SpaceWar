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
		}
		
		if (Event.current.type == EventType.MouseUp)
		{          
			targetPlanet = GetPlanetUserClickedOn();

			if(sourcePlanet != null && targetPlanet != null)
			{
				LaunchFleet();
			}
			else
			{
				Debug.Log("Reset planets");
				sourcePlanet = null;
				targetPlanet = null;
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

	void LaunchFleet ()
	{
		PlanetController sourcePlanetController = sourcePlanet.GetComponent<PlanetController>();
		PlanetController targetPlanetController = targetPlanet.GetComponent<PlanetController>();

		Debug.Log(string.Format("Launch fleet from planet {0} to planet {1}",sourcePlanetController.planetNumber,targetPlanetController.planetNumber));
	}
}
