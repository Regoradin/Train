using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo{
	//if things start breaking weirdly, it might be because you made this not inherit from monobehaviour, and you didn't entirely know what that meant. This was probably stupid.

	private string type;

	private float volume;
	public float Volume
	{
		get
		{
			return volume;
		}
	}

	private float mass;
	public float Mass { get; set; }

	private float value;
	public float Value
	{
		get
		{
			return value;
		}
	}

	private object handling_requirements; //this should be something more specific, maybe a string, not sure what though

	private float max_health;
	public float health;

	public Cargo(string type, float volume)
	{

		//this is where you can set up densities of mass, value, and health for various volumes/amounts of objects. Maybe in the future find some way to pull this from a config file or something.
		Dictionary<string, float> mass_density = new Dictionary<string, float>();
		mass_density["lumber"] = 1;

		Dictionary<string, float> value_density = new Dictionary<string, float>();
		value_density["lumber"] = 1;

		Dictionary<string, float> health_density = new Dictionary<string, float>();
		health_density["lumber"] = 1;

		this.type = type;
		this.volume = volume;

		try
		{
			mass = volume * mass_density[type];
		}
		catch (KeyNotFoundException)
		{
			mass = volume;
			Debug.Log("Cargo of type " + type + " mass density not found");
		}
		try
		{
			value = volume * value_density[type];
		}
		catch (KeyNotFoundException)
		{
			value = volume;
			Debug.Log("Cargo of type " + type + " value density not found");
		}
		try
		{
			max_health = volume * health_density[type];
		}
		catch (KeyNotFoundException)
		{
			max_health = volume;
			Debug.Log("Cargo of type " + type + " health density not found");
		}
		health = max_health;

	}

	public void Sell()
	{
		//sells the cargo or something, handle the unloading at the CargoController level, get money and here.
	}


}
