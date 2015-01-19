using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour 
{
	public int planetNumber;

	void Start()
	{
		DisableHalo();
	}

	void OnTriggerEnter(Collider other) 
	{
		if(!other.tag.Equals("Background"))
		{
			Debug.Log(string.Format("Collision with other planet! {0} , {1}",this, other));
		}
	}

	
	public void DisableHalo()
	{
		Light halo = this.GetComponentInChildren<Light>();
		halo.enabled = false;
	}
	
	public void EnableHalo()
	{
		Light halo = this.GetComponentInChildren<Light>();
		halo.enabled = true;		
	}

//
//	void OnMouseDown()
//	{
//		Debug.Log("Mouse down!");
//		TextMesh textLabel = this.GetComponentInChildren<TextMesh>();
//		textLabel.color = Color.blue;
//	}
//
//	void OnMouseUp()
//	{
//		Debug.Log("Mouse up!");
//		TextMesh textLabel = this.GetComponentInChildren<TextMesh>();
//		textLabel.color = Color.red;
//		Event.current.
//	}



}