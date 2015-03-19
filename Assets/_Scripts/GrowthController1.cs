using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrowthController1 : MonoBehaviour {

	public const float MAX_SIZE = 3f;
	public const float MIN_SIZE = 1.5f;
	public const int MIN_SHIPCOUNTER = 10;
	public const int MAX_SHIPCOUNTER = 100;

	public const float HALO_SIZE = 0.8f;

	public const float MIN_TIME_BETWEEN_GROWTH = 1.0f;

	public int SpaceShipCounter;

	private float currentGrowthTime;

	void Start () 
	{
		CalculateGrowthTime ();
		UpdateCounterDisplay ();
		SetHaloBySize ();
		StartCoroutine (GrowSpaceships());
	}

	IEnumerator GrowSpaceships()
	{
		while(true)
		{
			yield return new WaitForSeconds(currentGrowthTime);
			SpaceShipCounter++;
			UpdateCounterDisplay();
		}
	}

	void UpdateCounterDisplay()
	{
		Text displayCounter = this.GetComponentInChildren<Text> ();
		displayCounter.text = SpaceShipCounter + "";
	}

	void CalculateGrowthTime ()
	{
		float currentPlanetSize = this.transform.localScale.x;

		currentGrowthTime = MIN_TIME_BETWEEN_GROWTH * MAX_SIZE / currentPlanetSize;

	}

	void SetHaloBySize ()
	{
		Light halo = this.GetComponentInChildren<Light>();
		halo.range = this.transform.localScale.x + HALO_SIZE;
	}
}
