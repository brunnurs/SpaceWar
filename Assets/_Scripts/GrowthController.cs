﻿using UnityEngine;
using System.Collections;

public class GrowthController : MonoBehaviour
{
    /// <summary>
    /// All planets should have at least 10 ships on it, and max 100. The size grows from 1 to 3 
    /// </summary>
	public const float MAX_SIZE = 3f;
	public const float MIN_SIZE = 1f;
    public const float MIN_SHIPS = 10f;
    public const float MAX_SHIPS = 100f;

    public float timeBetweenGrowth;
    public float ShipCounter;

	private PlanetController planetController;

    void Start()
    {
		planetController = this.GetComponentInParent<PlanetController>();
        UpdateLabelAndSize();   
        StartCoroutine(GrowPlanet());
    }

    IEnumerator GrowPlanet()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenGrowth);

            if (ShipCounter < MAX_SHIPS)
            {
                ShipCounter++;
                UpdateLabelAndSize();
            }
        }
    }

    void UpdateLabelAndSize()
    {
        TextMesh textLabel = this.GetComponentInChildren<TextMesh>();
        textLabel.text = planetController.planetNumber + ":" + ShipCounter;

        Vector3 currentSize = new Vector3(MAX_SIZE / MAX_SHIPS * ShipCounter, MAX_SIZE / MAX_SHIPS * ShipCounter, 0);
        this.transform.localScale = currentSize;
    }
}
