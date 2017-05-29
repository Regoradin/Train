using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoController : MonoBehaviour {

	private TrainController train_controller;

	private List<Cargo> cargos;

	//the carriage that this cargo is loaded onto. Needed for physics and stuff
	public GameObject carriage;
	//the maximum amount of volume that can be put in the carriage
	public float max_volume;

	void Start()
	{
		train_controller = GetComponentInParent<TrainController>();

		cargos = new List<Cargo>();
	}

	public void AddCargo(Cargo new_cargo)
	{
		//adds the total volume of all of the cargo in the carriage
		float total_cargo_volume = 0;
		foreach (Cargo cargo in cargos)
		{
			total_cargo_volume += cargo.Volume;
		}

		//if there's space, add the new cargo
		if (total_cargo_volume + new_cargo.Volume <= max_volume)
		{
			//adds the cargo to the cargos list
			cargos.Add(new_cargo);
		}

		//and update total_cargo_volume and appearance of the carriage
		total_cargo_volume += new_cargo.Volume;
		UpdateAppearance();
	}

	/// <summary>
	/// removes a certain cargo object from the carriage cargo list. returns that cargo object. intended usage (i think): have some GUI elements made from the cargo list, each button having a cargo item assosciated with them. When clicked, it calls this fucntion.
	/// </summary>
	/// <param name="requested_cargo"> the cargo to be removed</param>
	/// <returns></returns>
	public Cargo RemoveCargo(Cargo requested_cargo)
	{
		//remove unwanted cargo from the list
		cargos.Remove(requested_cargo);

		//update looks and physics
		UpdateAppearance();
		UpdatePhysics();

		//and give it to whomever is trying to take it
		return requested_cargo;
	}

	void UpdateAppearance()
	{
		float total_cargo_volume = 0;
		foreach (Cargo cargo in cargos)
		{
			total_cargo_volume += cargo.Volume;
		}
		//more logic here for dealing with different types of cargo, maybe another mesh layed on top of the crates that has some symbol on it.
		if (total_cargo_volume > 0)
		{
			//make it look like there's a little cargo
		}
		if (total_cargo_volume > max_volume / 50)
		{
			//make it look like there's a decent amount of cargo
		}
		if (total_cargo_volume == max_volume)
		{
			//make it look like the thing is full
		}
	}

	void UpdatePhysics()
	{
		float total_cargo_mass = 0;
		foreach(Cargo cargo in cargos)
		{
			total_cargo_mass += cargo.Mass;
		}

		carriage.GetComponent<Rigidbody>().mass = total_cargo_mass + carriage.GetComponent<CarriageController>().empty_mass;
	}

	public void Damage(float damage)
	{
		foreach (Cargo cargo in cargos)
		{
			cargo.health -= damage;
			
			if(cargo.health <= 0)
			{
				RemoveCargo(cargo);
			}
		}
	}

	
}
