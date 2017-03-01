using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

	public delegate void CollisionAction(GameObject gameObject);
	public event CollisionAction OnCollision;

	//whenever a carriage collides with this track, sends and event to whomever is listening telling them that there is a carriage passing through
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Carriage")
		{
			if (OnCollision != null)
			{
				OnCollision(other.gameObject);
			}
		}
	}
}
