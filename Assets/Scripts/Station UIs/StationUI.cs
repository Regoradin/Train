using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class stationUI : MonoBehaviour {
	[HideInInspector]
	public TrainController train;

	public GameObject UI;

	[HideInInspector]
	public PlayerInventory inv;

	private void Start()
	{
		inv = GameObject.Find("Player").GetComponent<PlayerInventory>();
	}

	/// <summary>
	/// This is called whenever a train enters a station, and should set up any procedurally generated UI elements.
	/// </summary>
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
