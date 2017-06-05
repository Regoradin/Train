using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour {

	private TrainController train;
	public GameObject UI;

	private bool train_in_station;

	private Camera station_camera;
	private Camera player_camera;

	void Start()
	{
		player_camera = GameObject.Find("Player").GetComponentInChildren<Camera>();

		station_camera = GetComponentInChildren<Camera>();  //camera has to start enabled to get picked up by the script
		station_camera.enabled = false;
	}

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
				UI.SetActive(true);
				

				player_camera.enabled = false;
				station_camera.enabled = true;
			}
			else
			{
				UI.SetActive(false);

				player_camera.enabled = true;
				station_camera.enabled = false;
			}
		}
	}

	/// <summary>
	/// Launches the train from the station by giving it a little bit of velocity.
	/// </summary>
	public void Launch(float speed)
	{
		train.target_speed = speed;
	}

}
