using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	private Light halo;

	public void Awake()
	{
		halo = this.GetComponentInChildren<Light> ();
	}

	public void Start()
	{
		DisableHalo();
	}

	public void EnableHalo()
	{
		halo.enabled = true;
	}

	public void DisableHalo()
	{
		halo.enabled = false;
	}
}
