using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour {

	[HideInInspector]
	public bool Seated
	{
		get
		{
			return seated;
		}
	}
	bool seated = false;

	public Transform seated_transform;

	private Transform previous_transform;

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			if (Input.GetButtonDown("Interact"))
			{
				//attaching player to seat
				if (seated == false)
				{
					Debug.Log("seating");
					previous_transform = other.transform;
					other.transform.SetParent(transform);

					seated = true;

					other.GetComponent<PlayerAnimationController>().enabled = false;
					//other.GetComponent<CharacterController>().enabled = false;

					other.transform.position = seated_transform.position;
					other.transform.rotation = seated_transform.rotation;
				}

				else if(seated == true)
				{
					Debug.Log("deseating");
					other.transform.SetParent(previous_transform);

					seated = false;

					other.GetComponent<PlayerAnimationController>().enabled = true;
					//other.GetComponent<CharacterController>().enabled = true;
				}
			}
		}
	}


	private void Update()
	{

	}
}
