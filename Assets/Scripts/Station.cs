using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Station : MonoBehaviour {

	private TrainController train;
	public GameObject UI;

	public GameObject cargo_toggle_prefab;
	public GameObject cargo_toggle_group;

	private CargoController active_cargo;
	private int amount;

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
		if (train)
		{
			train_in_station = true;
		}

		//setting up the GUI button to select the active cargo carriage
		for(int i = 0; i < train.cargo_controllers.Count - 1; i++)
		{
			Debug.Log("placing button number " + i);
			GameObject cargo_toggle = Instantiate(cargo_toggle_prefab);
			cargo_toggle.transform.SetParent(cargo_toggle_group.transform);

			cargo_toggle.GetComponent<RectTransform>().localPosition = new Vector3(0, -i * 20, 0);
		}
	}

	void OnTriggerStay()
	{
		if (train_in_station)
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

	public void AddCargo(string type)
	{
		active_cargo.AddCargo(type, amount);
	}

	public void RemoveCargo(string type)
	{
		active_cargo.RemoveCargo(type, amount);
	}

	public void SetAmount(int _amount)
	{
		amount = _amount;
	}

	/// <summary>
	/// Launches the train from the station by giving it a little bit of velocity.
	/// </summary>
	public void Launch(float speed)
	{
		train.target_speed = speed;
	}

}
