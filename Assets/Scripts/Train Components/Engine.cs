using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour, IAiInteractable {

	private TrainController train_controller;

	public ParticleSystem smoke;
	public float smoke_emission_rate;

	private bool shoveling = false;

	public float coal;
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

	private bool task_requested = false;

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
				//some code here to trigger the shovel animation and handle when shovel should be called and set back to false and all that jazz.
				Shovel();
			}
			if (Input.GetKeyDown("k"))
			{
				Debug.Log("coal: " + coal);
				Debug.Log("fuel: " + fuel);
				Debug.Log("heat: " + heat);
				Debug.Log("speed: " + train_controller.local_speed);
			}

		}
	}

	public void AiInteract(GameObject ai, string type)
	{
		Shovel();
		task_requested = false;
	}

	private void Update()
	{
		//tell the crewmanager to send crew if fuel level drops below a certain amount. The type doesn't matter since the engine only has one assosciated action.
		if (fuel <= 30 && !task_requested)
		{
			train_controller.crew_manager.AddTask(new Task(gameObject, "refuel"));
			task_requested = true;
		}
	}

	private void FixedUpdate()
	{
		if (fuel > 0)
		{
			Burn();
		}

		if(heat > 0)
		{
			heat -= cooling_rate * Time.deltaTime;
		}


		//running the engine
		float engine_force = heat * engine_efficiency * Time.deltaTime;
		if (train_controller.target_speed > train_controller.local_speed && train_controller.local_speed > 0)
		{
			train_controller.AddForce(engine_force);
		}
		if(train_controller.target_speed < train_controller.local_speed && train_controller.local_speed < 0)
		{
			train_controller.AddForce(-engine_force);
		}

		var emission = smoke.emission;
		emission.rateOverTime = heat * smoke_emission_rate;

	}

	/// <summary>
	/// coal into fuel
	/// </summary>
	void Shovel()
	{
		if (!shoveling)
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
	}
	void StopShovel()
	{
		Debug.Log("Stopping shoveling");
		shoveling = false;
	}

	/// <summary>
	/// fuel into heat
	/// </summary>
	void Burn()
	{
		//fuel is reduced at 1 per second, regardless of engine characteristics, because the same piece of wood on fire will take the same length of time to burn. better engines will get more heat out of it though.
		fuel -= 1 * Time.deltaTime;
		heat += fuel_efficiency * Time.deltaTime;
	}

}
