using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour {
	//this script creates a one directional signal block between two track objects, and modifies the gameobject it is sitting on to indicate status

	public GameObject opening_track;
	public GameObject closing_track;

	public Color empty_block_color;
	public Color filled_block_color;

	private List<GameObject> carriages_in_block;

	void Start () {
		carriages_in_block = new List<GameObject>();

		//add removing and adding carriages to the approriate events
		opening_track.GetComponent<Track>().OnTrainCross += AddCarriage;
		closing_track.GetComponent<Track>().OnTrainCross += RemoveCarriage;
		UpdateState();
	}

	void AddCarriage(GameObject track, GameObject carriage)
	{
		carriages_in_block.Add(carriage);
		UpdateState();
	}

	void RemoveCarriage(GameObject track, GameObject carriage)
	{
		carriages_in_block.Remove(carriage);
		UpdateState();
	}

	void UpdateState()
	{
		Renderer rend = GetComponent<Renderer>();

		if(carriages_in_block.Count == 0)
		{
			rend.material.color = empty_block_color;
		}
		else
		{
			rend.material.color = filled_block_color;
		}
	}
}
