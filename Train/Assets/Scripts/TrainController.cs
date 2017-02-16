using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {

	private List<GameObject> carriages;

	public float top_speed;

	[HideInInspector]
	public float speed;
	[HideInInspector]
	public float target_speed;

	void Start()
	{

		target_speed = 100f;

		carriages = new List<GameObject>();

		foreach (Transform child in transform)
		{
			if(child.gameObject.tag == "Carriage")
			{
				carriages.Add(child.gameObject);
			}
		}

	}

	void Update()
	{
		speed = carriages[0].GetComponent<Rigidbody>().velocity.magnitude;
		Debug.Log("Speed: " + speed + " Target: " + target_speed);
	}

	/// <summary>
	/// Add a force to all carriages in the train
	/// </summary>
	/// <param name="force">The force to add</param>
	public void AddForce(float force)
	{
		foreach(GameObject carriage in carriages)
		{
			//Debug.Log("Adding force to " + carriage.name + force);
			carriage.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force);
		}
	}
}
