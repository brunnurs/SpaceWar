using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
	public float fleetPercentage;
	public Color playerColor;
	public string playerName;

	public override string ToString ()
	{
		return playerName;
	}
}
