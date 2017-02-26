using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour {
	//this script creates a one directional signal block between two track objects, and modifies the gameobject it is sitting on to indicate status

	public GameObject opening_track;
	public GameObject closing_track;

	private List<GameObject> carriages_in_block;

	void Start () {
		carriages_in_block = new List<GameObject>();
	}
	
}
