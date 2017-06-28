using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	public GameObject player;
	public float offset;

	private void Start()
	{
		if (!player)
		{
			Debug.Log("Map Camera is not assosciated with the player!");
		}
	}

	void Update ()
	{
		transform.position = player.transform.position + offset * Vector3.up;
	}
}
