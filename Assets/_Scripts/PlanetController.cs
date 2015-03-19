using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public void Start()
	{
		DisableHalo();
	}

	public void EnableHalo()
	{
		var halo = this.GetComponentInChildren<Light> ();
		halo.enabled = true;
	}

	public void DisableHalo()
	{
		var halo = this.GetComponentInChildren<Light> ();
		halo.enabled = false;
	}
}
