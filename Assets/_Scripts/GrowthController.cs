using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrowthController : MonoBehaviour
{
    /// <summary>
    /// All planets should have at least 10 ships on it, and max 100. The size grows from 1 to 3 
    /// </summary>
	public const float MAX_SIZE = 3f;
	public const float MIN_SIZE = 2f;
    public const float MIN_SHIPS = 10f;
    public const float MAX_SHIPS = 100f;

	private const float HALO_SIZE = 0.8f;

    public float timeBetweenGrowth;
    public float ShipCounter;

    void Start()
    {
        UpdateLabelAndSizeAndHalo();

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
                UpdateLabelAndSizeAndHalo();
            }
        }
    }

	public int ReduceShips(int ships)
	{
		int remainingShips = 0;

		if (ShipCounter - ships < 0)
		{
			remainingShips = (int)Mathf.Abs(ShipCounter - ships);
			ShipCounter = 0;
		}
		else
		{
			ShipCounter -= ships;
		}


		UpdateLabelAndSizeAndHalo();

		return remainingShips;
	}

	public void AddShips(int ships)
	{
		ShipCounter += ships;
		ShipCounter = Mathf.Min(ShipCounter,MAX_SHIPS);

		UpdateLabelAndSizeAndHalo();
	}

    void UpdateLabelAndSizeAndHalo()
    {
        Text textLabel = this.GetComponentInChildren<Text>();
        textLabel.text = ShipCounter.ToString();

		float newDiameter = MAX_SIZE / MAX_SHIPS * ShipCounter;

		if(newDiameter < MIN_SIZE)
		{
			newDiameter = MIN_SIZE;
		}

        Vector3 currentSize = new Vector3(newDiameter,newDiameter,newDiameter);
        this.transform.localScale = currentSize;

		Light halo = this.GetComponentInChildren<Light>();
		halo.range = newDiameter + HALO_SIZE;
    }
}
