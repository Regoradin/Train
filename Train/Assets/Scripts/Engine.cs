using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

	private TrainController train_controller;

	private bool shoveling = false;

	private float coal;
	private float fuel;
	private float heat;

	public float cooling_rate;

	public float max_coal;
	public float max_fuel;
	//how much coal you can shovel at a time
	public float shovel_efficiency;
	//how efficiently your fuel is turned to heat
	public float fuel_efficiency;
	//how efficiently your heat is turned into motion
	public float engine_efficiency;

	void Start () {

		train_controller = transform.GetComponentInParent<TrainController>();

		coal = max_coal;
		fuel = 0;
		heat = 0;

	}


	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			if (Input.GetButtonDown("Interact"))
			{
				if (shoveling == false)
				{
					//some code here to trigger the shovel animation and handle when shovel should be called and set back to false and all that jazz.
					Shovel();
				}
			}
			if (Input.GetKeyDown("k"))
			{
				Debug.Log("coal: " + coal);
				Debug.Log("fuel: " + fuel);
				Debug.Log("heat: " + heat);
				Debug.Log("speed: " + train_controller.speed);
			}

		}
	}

	void Update()
	{
		if (fuel > 0)
		{
			Burn();
		}

		if(heat > 0)
		{
			heat -= cooling_rate * Time.deltaTime;
		}

		if(train_controller.speed < train_controller.target_speed)
		{
			Debug.Log("going");
			train_controller.AddForce(heat * engine_efficiency * Time.deltaTime);
		}
	}

	void Shovel()
	{
		if (fuel + shovel_efficiency < max_fuel)
		{
			shoveling = true;
			Debug.Log("shoveling");

			coal -= shovel_efficiency;
			fuel += shovel_efficiency;

			Invoke("StopShovel", 1);
		}
	}
	void StopShovel()
	{
		Debug.Log("Stopping shoveling");
		shoveling = false;
	}

	void Burn()
	{
		//fuel is reduced at 1 per second, regardless of engine characteristics, because the same piece of wood on fire will take the same length of time to burn. better engines will get more heat out of it though.
		fuel -= 1 * Time.deltaTime;
		heat += fuel_efficiency * Time.deltaTime;
	}

}
