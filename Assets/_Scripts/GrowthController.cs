using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrowthController : MonoBehaviour
{
	public const float MAX_SIZE = 3f;
	public const float MIN_SIZE = 1.5f;
    public const int MIN_SHIPS = 10;
    public const int MAX_SHIPS = 100;

	public const float HALO_SIZE = 0.8f;
	public const float MIN_TIME_BETWEEN_GROWTH = 1.0f;

	public float ShipCounter;

    private float timeBetweenGrowth;

    void Start()
    {
		CalculateTimeBetweenGrowth();

		SetHaloBySize();

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
    }

	void CalculateTimeBetweenGrowth ()
	{
		float planetSize = this.transform.localScale.x;

		this.timeBetweenGrowth = MIN_TIME_BETWEEN_GROWTH * MAX_SIZE / planetSize;
		Debug.Log(string.Format("Time between Growth {0}",timeBetweenGrowth));

	}

	void SetHaloBySize ()
	{
		Light halo = this.GetComponentInChildren<Light>();
		halo.range = this.transform.localScale.x + HALO_SIZE;
	}
}
