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
	}


	public void AddForce(float force)
	{
		foreach(GameObject carriage in carriages)
		{
			if(carriage.GetComponent<Rigidbody>().velocity.magnitude < top_speed)
			{
				carriage.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force);
			}
		}
	}
}
