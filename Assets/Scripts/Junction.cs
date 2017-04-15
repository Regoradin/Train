using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour {

	//Gameobjects which have the tracks comprising the branches as their children
	public GameObject entrance_parent;
	public GameObject left_parent;
	public GameObject center_parent;
	public GameObject right_parent;

	private List<GameObject> carriages_in_junction;
	private TrainController train_controller;

	//dictionary with each path identified by string name and a list of the gameobjects comprising them
	private Dictionary<string, List<GameObject>> paths;

	private void Start()
	{
		//initializing stuff
		carriages_in_junction = new List<GameObject>();
		paths = new Dictionary<string, List<GameObject>>();

		//Sets up paths to each have their exit and the central entrance branch
		paths["left"] = GetTracks(left_parent);
		paths["center"] = GetTracks(center_parent);
		paths["right"] = GetTracks(right_parent);

		foreach(string key in paths.Keys)
		{
			paths[key].AddRange(GetTracks(entrance_parent));
		}

	}

	private List<GameObject> GetTracks(GameObject parent)
	{
		List<GameObject> tracks = new List<GameObject>();


		//builds a list of all the children that are tracks
		foreach(Transform child in parent.transform)
		{
			if(child.tag == "Track")
			{
				tracks.Add(child.gameObject);
			}
		}

		return tracks;
	}
	
}
