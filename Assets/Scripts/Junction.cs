using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour {

	public GameObject in_track;
	public GameObject left_out_track;
	public GameObject center_out_track;
	public GameObject right_out_track;

	private Collider in_collider;
	private Collider left_out_collider;
	private Collider center_out_collider;
	private Collider right_out_collider;

	private List<GameObject> junction_tracks;

	private List<GameObject> carriages_in_junction;

	void Start()
	{
		//subcsribes all the things to the OnCollision event if they exist
		carriages_in_junction = new List<GameObject>();

		//setting up colliders
		in_collider = SetUpColliders(in_track);
		left_out_collider = SetUpColliders(left_out_track);
		center_out_collider = SetUpColliders(center_out_track);
		right_out_collider = SetUpColliders(right_out_track);

		//setting up the list of colliders
		junction_tracks = new List<GameObject>();
		junction_tracks.Add(in_track);
		junction_tracks.Add(left_out_track);
		junction_tracks.Add(center_out_track);
		junction_tracks.Add(right_out_track);

		//setting up events
		Debug.Log("this does actually happen");
		in_track.GetComponent<Track>().OnTrainCross += EnterJunction;
		left_out_track.GetComponent<Track>().OnTrainCross += EnterJunction;
		center_out_track.GetComponent<Track>().OnTrainCross += EnterJunction;
		right_out_track.GetComponent<Track>().OnTrainCross += EnterJunction;

	}

	Collider SetUpColliders(GameObject track)
	{
		//quick little setup function to return the trigger collider on the track
		foreach (Collider collider in track.GetComponents<Collider>())
		{
			if (collider.isTrigger)
			{
				return collider;
			}
		}
		Debug.Log("Junction " + name + " couldn't find a collider.");
		return new Collider(); //This should never happen, if it does, panic
	}

	void EnterJunction(GameObject track, GameObject carriage)
	{
		Debug.Log("entering junction");
		if(carriages_in_junction.Count == 0)
		{
							//if the junction is currently empty, i.e. it is still in an accepting new trains states, switch the subscribed functions
			if (track == in_track)
			{
				//if it is traversing the junction the correct direction:
				foreach(GameObject junction_track in junction_tracks)
				{
					if (junction_track != in_track)
					{
						junction_track.GetComponent<Track>().OnTrainCross -= EnterJunction;
						junction_track.GetComponent<Collider>().enabled = false;
					}
				}

				string direction = carriage.GetComponent<CarriageController>().direction;
				if (direction == "left")
				{
					left_out_track.GetComponent<Track>().OnTrainCross += LeaveJunction;
					left_out_collider.enabled = true;
				}
				if (direction == "center")
				{
					center_out_track.GetComponent<Track>().OnTrainCross += LeaveJunction;
					center_out_collider.enabled = true;
				}
				if (direction == "right")
				{
					right_out_track.GetComponent<Track>().OnTrainCross += LeaveJunction;
					right_out_collider.enabled = true;
				}
			}

			else
			{
				//if it is traversing the track in the wrong direction:
				foreach (GameObject junction_track in junction_tracks)
				{
					if(junction_track != track)
					{
						junction_track.GetComponent<Track>().OnTrainCross -= EnterJunction;
						junction_track.GetComponent<Collider>().enabled = false;
					}
					if(junction_track == in_track)
					{
						junction_track.GetComponent<Track>().OnTrainCross += LeaveJunction;
						junction_track.GetComponent<Collider>().enabled = true;
					}
				}
			}

		}
		carriages_in_junction.Add(carriage);
	}

	void LeaveJunction(GameObject track, GameObject carriage)
	{
		carriages_in_junction.Remove(carriage);

		if(carriages_in_junction.Count == 0)
		{
			foreach (GameObject junction_track in junction_tracks)
			{
				//sets everything back to normal
				junction_track.GetComponent<Track>().OnTrainCross -= LeaveJunction;
				junction_track.GetComponent<Track>().OnTrainCross -= EnterJunction;     //this is just to remove the enter junction from the one track that was being entered through, to prevent doubling up functions
				junction_track.GetComponent<Track>().OnTrainCross += EnterJunction;

				junction_track.GetComponent<Collider>().enabled = true;
			}
		}
	}
	
}
