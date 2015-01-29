using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrowthController : MonoBehaviour
{
	public const float MAX_SIZE = 3f;
	public const float MIN_SIZE = 2f;
    public const float MIN_SHIPS = 10f;
    public const float MAX_SHIPS = 100f;

	public const float HALO_SIZE = 0.8f;
	public const float MIN_TIME_BETWEEN_GROWTH = 1;
	public const float MAX_TIME_BETWEEN_GROWTH = 2.5;

	public float ShipCounter;

    private float timeBetweenGrowth;

    void Start()
    {
		CalculateTimeBetweenGrowth();

        UpdateLabel();

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
                UpdateLabel();
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


		UpdateLabel();

		return remainingShips;
	}

	public void AddShips(int ships)
	{
		ShipCounter += ships;
		ShipCounter = Mathf.Min(ShipCounter,MAX_SHIPS);

		UpdateLabel();
	}

    void UpdateLabel()
    {
        Text textLabel = this.GetComponentInChildren<Text>();
        textLabel.text = ShipCounter.ToString();

//		float newDiameter = MAX_SIZE / MAX_SHIPS * ShipCounter;
//
//		if(newDiameter < MIN_SIZE)
//		{
//			newDiameter = MIN_SIZE;
//		}
//
//        Vector3 currentSize = new Vector3(newDiameter,newDiameter,newDiameter);
//        this.transform.localScale = currentSize;
//
//		Light halo = this.GetComponentInChildren<Light>();
//		halo.range = newDiameter + HALO_SIZE;
    }

	void CalculateTimeBetweenGrowth ()
	{
		float planetSize = this.transform.localScale.x;

		this.timeBetweenGrowth = MAX_TIME_BETWEEN_GROWTH / MAX_SIZE * planetSize;

	}
}
