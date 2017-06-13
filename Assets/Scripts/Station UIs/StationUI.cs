using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class stationUI : MonoBehaviour {
	[HideInInspector]
	public TrainController train;

	public GameObject UI;

	//this is intended to set up any procedurally generated UI elements
	public abstract void SetupUI();


	/// <summary>
	/// Launches the train from the station by setting the target speed to the given value.
	/// </summary>
	/// <param name="speed"></param>
	public void Launch(float speed)
	{
		train.target_speed = speed;
	}

}
