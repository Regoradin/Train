using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour {

	//Gameobjects which have the tracks comprising the branches as their children. The parent GameObject is the opening/closing track for that branch
	public GameObject entrance_parent;
	public GameObject left_parent;
	public GameObject center_parent;
	public GameObject right_parent;

	private float initial_speed_direction;  //-1 or 1, depending on the sign of the velocity when entering the junction

	private List<GameObject> carriages_in_junction;
	private TrainController train_controller;

	//dictionary with each path identified by string name and a list of the gameobjects comprising them
	private Dictionary<string, List<GameObject>> paths;
	private Dictionary<string, GameObject> entrances;

	private void Start()
	{
		//initializing stuff
		carriages_in_junction = new List<GameObject>();
		paths = new Dictionary<string, List<GameObject>>();
		entrances = new Dictionary<string, GameObject>();

		//In case any of the paths aren't defined, sets them to center or right, whichever exists
		if (!center_parent)
		{
			center_parent = right_parent;
		}
		if (!right_parent)
		{
			right_parent = center_parent;
		}
		if (!left_parent)
		{
			left_parent = center_parent;
		}

		//Sets up paths to each have their exit and the central entrance branch
		paths["left"] = GetTracks(left_parent);
		paths["center"] = GetTracks(center_parent);
		paths["right"] = GetTracks(right_parent);

		foreach(string key in paths.Keys)
		{
			paths[key].AddRange(GetTracks(entrance_parent));
		}

		//Sets up entrances
		entrances["entrance"] = entrance_parent;
		entrances["left"] = left_parent;
		entrances["center"] = center_parent;
		entrances["right"] = right_parent;

		//Add events to listen for entraces to the junction. If blocks prevent double event assignment when there are not 3 exit paths
		entrance_parent.GetComponent<Track>().OnTrainEnter += Enter;
		left_parent.GetComponent<Track>().OnTrainEnter += Enter;
		center_parent.GetComponent<Track>().OnTrainEnter += Enter;
		if (left_parent == center_parent)
		{
			center_parent.GetComponent<Track>().OnTrainEnter -= Enter;
		}
		right_parent.GetComponent<Track>().OnTrainEnter += Enter;
		if(right_parent == center_parent)
		{
			center_parent.GetComponent<Track>().OnTrainEnter -= Enter;
		}


	}


	private List<GameObject> GetTracks(GameObject parent)
	{
		List<GameObject> tracks = new List<GameObject>();

		//adds itself to the list
		if (parent.tag == "Track")
		{
			tracks.Add(parent);

		//builds a list of all the children that are tracks
			foreach (Transform child in parent.transform)
			{
				if (child.tag == "Track")
				{
					tracks.Add(child.gameObject);
				}
			}
		}
		return tracks;
	}


	private void Enter(GameObject entered_track, GameObject carriage)
	{
		//sets up direction, which is basically the path the train is taking
		string direction = "";
		if (entered_track == entrance_parent)
		{
			direction = carriage.GetComponentInParent<TrainController>().Direction;
		}
		if(entered_track == left_parent)
		{
			direction = "left";
		}
		if(entered_track == center_parent)
		{
			direction = "center";
		}
		if(entered_track == right_parent)
		{
			direction = "right";
		}

		Debug.Log("direction is set to: " + direction);

		initial_speed_direction = Mathf.Sign(carriage.GetComponentInParent<TrainController>().local_speed);

		//turning off all tracks, and then only turn on appropriate track
		foreach (string key in paths.Keys)
		{
			foreach(GameObject track in paths[key])
			{
				track.GetComponent<Collider>().enabled = false;
			}
		}

		foreach(GameObject track in paths[direction])
		{
			track.GetComponent<Collider>().enabled = true;
		}

		//adjusting event listening on the chosen path 
		//removing the waiting for entrance events
		entrances["entrance"].GetComponent<Track>().OnTrainEnter -= Enter;
		entrances[direction].GetComponent<Track>().OnTrainEnter -= Enter;
		//adding events for the train being in the junction
		if (entered_track == entrances["entrance"])
		{
			entrances["entrance"].GetComponent<Track>().OnTrainExit += OpenEnter;
			entrances[direction].GetComponent<Track>().OnTrainExit += OpenExit;
		}
		else
		{
			entrances["entrance"].GetComponent<Track>().OnTrainExit += OpenExit;
			entrances[direction].GetComponent<Track>().OnTrainExit += OpenEnter;
		}

	}


	//OpenEnter and OpenExit are functions that listen to carriages entering and leaving a junction that has already been opened. As such, entered_track should be the track that is the entrance or exit, respectively
	private void OpenEnter(GameObject entered_track, GameObject carriage)
	{
		float carriage_speed_direction = Mathf.Sign(carriage.GetComponentInParent<TrainController>().local_speed);
		if (carriage_speed_direction == initial_speed_direction)
		{
			Debug.Log("entering " + entered_track.name);
			carriages_in_junction.Add(carriage);
		}
		if(carriage_speed_direction != initial_speed_direction)
		{
			carriages_in_junction.Remove(carriage);
		}
		if (carriages_in_junction.Count == 0)  //When the junction is empty, call Reset on all active entrances
		{
			Reset();
		}

	}

	private void OpenExit(GameObject entered_track, GameObject carriage)
	{
		float carriage_speed_direction = Mathf.Sign(carriage.GetComponentInParent<TrainController>().local_speed);
		if (carriage_speed_direction == initial_speed_direction)
		{
			Debug.Log("Exiting " + entered_track.name);
			carriages_in_junction.Remove(carriage);
		}
		if (carriage_speed_direction != initial_speed_direction)
		{
			carriages_in_junction.Add(carriage);
		}

		if (carriages_in_junction.Count == 0)  //When the junction is empty, call Reset on all active entrances
		{
			Reset();
		}
	}

	/// <summary>
	/// Resets the entire junction back to it's normal state.
	/// </summary>
	private void Reset()
	{
		Debug.Log("resetting");
		foreach (KeyValuePair<string, GameObject> entrance in entrances)
		{
			if (entrance.Value.activeSelf == true)
			{
				Track track_script = entrance.Value.GetComponent<Track>();
				track_script.OnTrainExit -= OpenEnter;
				track_script.OnTrainExit -= OpenExit;

				track_script.OnTrainEnter += Enter;
			}
		}
		//turn deactivated paths back on
		foreach (string key in paths.Keys)
		{
			foreach (GameObject track in paths[key])
			{
				track.GetComponent<Collider>().enabled = true;
			}
		}
	}
}
