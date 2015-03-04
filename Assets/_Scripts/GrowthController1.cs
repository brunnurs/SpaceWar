using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrowthController1 : MonoBehaviour {

	public int SpaceShipCounter;

	void Start () 
	{
		SpaceShipCounter = 10;
		UpdateCounterDisplay ();
		StartCoroutine (GrowSpaceships());
	}

	IEnumerator GrowSpaceships()
	{
		while(true)
		{
			yield return new WaitForSeconds(1);
			SpaceShipCounter++;
			UpdateCounterDisplay();
		}
	}

	void UpdateCounterDisplay()
	{
		Text displayCounter = this.GetComponentInChildren<Text> ();
		displayCounter.text = SpaceShipCounter + "";
	}

}
