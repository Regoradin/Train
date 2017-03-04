using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

	public delegate void TrainCrossing(GameObject track, GameObject carriage);
	public event TrainCrossing OnTrainCross;

	//whenever a carriage collides with this track, sends and event to whomever is listening telling them that there is a carriage passing through
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Carriage")
		{
			if (OnTrainCross != null)
			{
				OnTrainCross(gameObject, other.gameObject);
			}
		}
	}
}
