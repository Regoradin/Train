using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Station : MonoBehaviour{

	private Camera station_camera;
	private Camera player_camera;

	private stationUI UI_script;

	private TrainController train;

	private bool train_in_station;
	private List<GameObject> carriages_in_station;

	void Start ()
	{
		player_camera = GameObject.Find("Player").GetComponentInChildren<Camera>();

		UI_script = GetComponentInChildren<stationUI>();

		station_camera = GetComponentInChildren<Camera>();  //camera has to start enabled to get picked up by the script
		station_camera.enabled = false;

		carriages_in_station = new List<GameObject>();

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Carriage"))
		{
			carriages_in_station.Add(other.gameObject);

			train = other.GetComponentInParent<TrainController>();
			UI_script.train = train;
			train_in_station = true;

			if (carriages_in_station.Count == 1)
			{
				UI_script.SetupUI();
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Carriage"))
		{
			carriages_in_station.Remove(other.gameObject);
			if(carriages_in_station.Count == 0)
			{
				train_in_station = false;
			}
		}
	}

	void OnTriggerStay()
	{
		if (train_in_station)
		{
			if (train.local_speed == 0)
			{
				player_camera.enabled = false;
				station_camera.enabled = true;

				UI_script.UI.SetActive(true);
			}
			else
			{
				player_camera.enabled = true;
				station_camera.enabled = false;

				UI_script.UI.SetActive(false);
			}
		}
	}


}
