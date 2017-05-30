using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour {

	private TrainController train;
	public Canvas UI;

	private bool train_in_station;

	void OnTriggerEnter(Collider other)
	{
		train = other.GetComponentInParent<TrainController>();
	}

	void OnTriggerStay()
	{
		if (train)
		{
			if(train.local_speed == 0)
			{
				UI.enabled = true;
			}
			else
			{
				UI.enabled = false;
			}
		}
	}

}
