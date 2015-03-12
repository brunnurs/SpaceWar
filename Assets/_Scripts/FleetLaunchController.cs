using UnityEngine;
using System.Collections;

public class FleetLaunchController : MonoBehaviour {

	GameController gameController;

	void Awake()
	{
		gameController = this.GetComponentInParent<GameController>();
	}


	void OnGUI()
	{
		if (Event.current.type == EventType.MouseDown) 
		{
			GameObject planet = GetPlanetClickedOn();

			if(planet == null)
			{
				Debug.Log("No planet hitted!");
			}
			else
			{

				Debug.Log("Planet hitted!");
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
