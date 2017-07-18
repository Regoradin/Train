using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossing : MonoBehaviour
{
	private Track this_track;
	public Track other_track;

	private List<GameObject> carriages_in_block;
	private List<GameObject> other_carriages_in_block;

	private void Start()
	{
		carriages_in_block = new List<GameObject>();
		other_carriages_in_block = new List<GameObject>();

		this_track = GetComponent<Track>();
		other_track.OnTrainEnter += OtherEnter;
		other_track.OnTrainExit += OtherExit;
	}

	private void OnTriggerEnter(Collider other)
	{
		carriages_in_block.Add(other.gameObject);
		other_track.GetComponent<Collider>().enabled = false;
	}

	private void OnTriggerExit(Collider other)
	{
		carriages_in_block.Remove(other.gameObject);
		if(carriages_in_block.Count == 0)
		{
			other_track.GetComponent<Collider>().enabled = true;
		}
	}

	private void OtherEnter(GameObject track, GameObject carriage)
	{
		other_carriages_in_block.Add(carriage);
		this_track.GetComponent<Collider>().enabled = false;
	}

	private void OtherExit(GameObject track, GameObject carriage)
	{
		other_carriages_in_block.Remove(carriage);
		if(other_carriages_in_block.Count == 0)
		{
			this_track.GetComponent<Collider>().enabled = true;
		}
	}

}