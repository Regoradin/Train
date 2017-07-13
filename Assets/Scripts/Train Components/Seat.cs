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
	public GameObject seated_gameobject
	{
		get
		{
			return previous_transform.gameObject;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			//with this setup if the player tries to sit, either they will sit or kick the crew out of the seat.
			if (Input.GetButtonDown("Interact"))
			{
				if (!seated)
				{
					TrySeat(other.gameObject);
				}
				else
				{
					TryDeseat();
				}
			}
		}
	}

	/// <summary>
	/// Attempts to seat a gameobject in the seat. Returns false if the seat is already occupied.
	/// </summary>
	/// <param name="other"></param>
	/// <returns></returns>
	public bool TrySeat(GameObject other)
	{
		if (seated == false)
		{
			previous_transform = other.transform;
			other.transform.SetParent(transform);

			seated = true;

			if (other.CompareTag("Player"))
			{
				other.GetComponent<PlayerAnimationController>().enabled = false;
			}

			other.transform.position = seated_transform.position;
			other.transform.rotation = seated_transform.rotation;

			return true;
		}

		else
		{
			return false;
		}
	}

	/// <summary>
	/// Attempts to deseat the current sitter. Returns false if nobody is sitting.
	/// </summary>
	/// <param name="other"></param>
	/// <returns></returns>
	public bool TryDeseat()
	{
		if (seated == true)
		{
			if (seated_gameobject.GetComponent<Crew>())
			{
				seated_gameobject.GetComponent<Crew>().busy = false;
			}
			seated_gameobject.transform.SetParent(previous_transform);	

			seated = false;

			if (seated_gameobject.CompareTag("Player"))
			{
				seated_gameobject.GetComponent<PlayerAnimationController>().enabled = true;
			}

			return true;
		}

		else
		{
			return false;
		}
	}

}
