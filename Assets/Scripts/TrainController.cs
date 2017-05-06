using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {

	private List<GameObject> carriages;

	public float top_speed;

	[HideInInspector]
	public float local_speed;
	[HideInInspector]
	public float target_speed = 0f;

	private string direction;
	[HideInInspector]
	public string Direction
	{
		get
		{
			return direction;
		}
		set
		{
			if (value == "left" || value == "center" || value == "right")
			{
				direction = value;
				UpdateCarriageDirection(value);
			}
			else
			{
				Debug.Log("Invalid direction " + value + " tried to set as train " + name + " direction");
			}
		}
	}

	void Start()
	{
		//builds a list of all the carriages in the same order as they are in the inspector
		carriages = new List<GameObject>();
		foreach (Transform child in transform)
		{
			if(child.gameObject.tag == "Carriage")
			{
				carriages.Add(child.gameObject);
			}
		}

		UpdateCarriageDirection(direction);			//this has to be done at the start to set direction, but after the carriages list is built

	}

	void Update()
	{
		local_speed = carriages[0].transform.InverseTransformDirection(carriages[0].GetComponent<Rigidbody>().velocity).z;

		//changes the local forward velocity of each carriage to match the lead carriages speed
		foreach (GameObject carriage in carriages)
		{
			Vector3 world_speed = carriage.transform.TransformDirection(local_speed * Vector3.forward);
			carriage.GetComponent<Rigidbody>().velocity = world_speed;
		}
	}

	/// <summary>
	/// Add a force to all carriages in the train. This should probably only be called by the engine to go forward, and the speedController to apply breaks
	/// </summary>
	/// <param name="force">The force to add</param>
	public void AddForce(float force)
	{
		foreach(GameObject carriage in carriages)
		{
			carriage.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force);
		}
	}
	
	void UpdateCarriageDirection(string direction)
	{
		//updates the direction of each carriage for traversing junctions
		foreach(GameObject carriage in carriages)
		{
			carriage.GetComponent<CarriageController>().direction = direction;
		}
	}
}
