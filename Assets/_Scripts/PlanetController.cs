using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour 
{
	public int planetNumber;

	private PlayerController currentOwner;

	private GrowthController growthController;

	/// <summary>
	/// We need to use the Awake-Method because the  Start() method is called after the Player is set and would therefore remove the halo also for this planet
	/// </summary>
	void Awake()
	{
		growthController = gameObject.GetComponent<GrowthController>();

		Light halo = this.GetComponentInChildren<Light>();
		halo.enabled = false;
	}

	void OnTriggerEnter(Collider other) 
	{

		if(other.gameObject.tag.Equals("Spaceship"))
		{
			//It could be that we just cross another planet on our route. TODO: let the ship fly around the planet instead just through it
			SpaceshipController spaceship =	other.GetComponentInParent<SpaceshipController>();

			if(spaceship.targetPlanet == this.gameObject)
			{
				SpaceshipAttacksPlanet (spaceship);
			}

		}
	}

	void SpaceshipAttacksPlanet (SpaceshipController spaceship)
	{
		if (spaceship.shipOwner == currentOwner) 
		{
			Debug.Log(string.Format("Spaceship from player {0} supports another planet",spaceship.shipOwner));
			growthController.AddShips (spaceship.fleetSize);
		}
		else 
		{
			Debug.Log(string.Format("Spaceship from player {0} attacks a planet!",spaceship.shipOwner));
			int remainingShips = growthController.ReduceShips (spaceship.fleetSize);

			if (remainingShips > 0) 
			{
				PlanetTakeOver (remainingShips, spaceship.shipOwner);
			}
		}

		Destroy(spaceship.gameObject);
	}

	void PlanetTakeOver (int remainingShips, PlayerController shipOwner)
	{
		growthController.AddShips(remainingShips);
		CurrentOwner = shipOwner;
	}
	
	public void DisableHalo()
	{
//		Light halo = this.GetComponentInChildren<Light>();
//		halo.enabled = false;
	}
	
	public void EnableHalo()
	{
//		Light halo = this.GetComponentInChildren<Light>();
//		halo.enabled = true;		
	}

	public void SetHaloColor(Color color)
	{
		Light halo = this.GetComponentInChildren<Light>();
		//Don't know why it is not just possible to set the color. Solution releated to this thread http://answers.unity3d.com/questions/527639/light-halo-and-color.html
		halo.color = new Color(color.r,color.g,color.b,1);
		halo.enabled = true;		
	}

	public PlayerController CurrentOwner
	{
		set
		{
			currentOwner = value;
			SetHaloColor(currentOwner.playerColor);
		}
		get
		{
			return currentOwner;
		}
	}

}