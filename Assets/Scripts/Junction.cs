using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour {

	public List<GameObject> in_tracks;
	public List<GameObject> left_out_tracks;
	public List<GameObject> center_out_tracks;
	public List<GameObject> right_out_tracks;

	private List<Collider> in_colliders;
	private List<Collider> left_out_colliders;
	private List<Collider> center_out_colliders;
	private List<Collider> right_out_colliders;

	private List<GameObject> junction_tracks;

	private List<GameObject> carriages_in_junction;

	void Start()
	{
		//subcsribes all the things to the OnCollision event if they exist
		carriages_in_junction = new List<GameObject>();

		//setting up colliders
		in_colliders = SetUpColliders(in_tracks);
		left_out_colliders = SetUpColliders(left_out_tracks);
		center_out_colliders = SetUpColliders(center_out_tracks);
		right_out_colliders = SetUpColliders(right_out_tracks);

		//assembling junction_tracks
		junction_tracks.AddRange(in_tracks);
		junction_tracks.AddRange(left_out_tracks);
		junction_tracks.AddRange(center_out_tracks);
		junction_tracks.AddRange(right_out_tracks);


		//setting up events, it should work to just have the first one be the one that is in the list, but maybe not. Hopefully remember to order lists with the farthest out track first
		in_tracks[0].GetComponent<Track>().OnTrainCross += EnterJunction;
		left_out_tracks[0].GetComponent<Track>().OnTrainCross += EnterJunction;
		center_out_tracks[0].GetComponent<Track>().OnTrainCross += EnterJunction;
		right_out_tracks[0].GetComponent<Track>().OnTrainCross += EnterJunction;

	}

	List<Collider> SetUpColliders(List<GameObject> tracks)
	{	
		//quick little setup function to return the trigger collider on the track

		List<Collider> colliders = new List<Collider>();

		foreach (GameObject track in tracks)
		{
			foreach (Collider collider in track.GetComponents<Collider>())
			{
				if (collider.isTrigger)
				{
					colliders.Add(collider);
				}
			}
		}
		return colliders;
	}

	void EnterJunction(GameObject track, GameObject carriage)
	{
		//called when a carriage enters a junction
		Debug.Log(carriage.name + " entering junction");
		if(carriages_in_junction.Count == 0)
		{
			//if the junction is currently empty, i.e. it is still in an accepting new trains states, switch the subscribed functions
			if (in_tracks.Contains(track))
			{
				//if it is traversing the junction the correct direction, turn off everything on the outgoing branches:
				foreach (GameObject junction_track in junction_tracks)
				{
					if (!in_tracks.Contains(junction_track))
					{
						junction_track.GetComponent<Track>().OnTrainCross -= EnterJunction;
						junction_track.GetComponent<Collider>().enabled = false;
					}
				}

				string direction = carriage.GetComponent<CarriageController>().direction;
				if (direction == "left")
				{
					foreach (GameObject individual_track in left_out_tracks)
					{
						individual_track.GetComponent<Track>().OnTrainCross += LeaveJunction;
					}
					foreach (Collider collider in left_out_colliders)
					{
						collider.enabled = true;
					}
				}
				if (direction == "center")
				{
					foreach (GameObject individual_track in center_out_tracks)
					{
						individual_track.GetComponent<Track>().OnTrainCross += LeaveJunction;
					}
					foreach (Collider collider in center_out_colliders)
					{
						collider.enabled = true;
					}
				}
				if (direction == "right")
				{
					foreach (GameObject individual_track in right_out_tracks)
					{
						individual_track.GetComponent<Track>().OnTrainCross += LeaveJunction;
					}
					foreach (Collider collider in right_out_colliders)
					{
						collider.enabled = true;
					}
				}
			}

			else
			{
				//if it is traversing the track in the wrong direction:

				//figures out which tracks were entered from
				List<GameObject> entered_tracks = null;
				if (left_out_tracks.Contains(track))
				{
					entered_tracks = left_out_tracks;
				}
				if (center_out_tracks.Contains(track))
				{
					entered_tracks = center_out_tracks;
				}
				if (right_out_tracks.Contains(track))
				{
					entered_tracks = right_out_tracks;
				}

				foreach (GameObject junction_track in junction_tracks)
				{
					
					//turns off superflous branches
					if (!entered_tracks.Contains(junction_track))
					{
						junction_track.GetComponent<Track>().OnTrainCross -= EnterJunction;
						junction_track.GetComponent<Collider>().enabled = false;
					}
					//turns on the in branch, which is being used for an exit
					if (in_tracks.Contains(junction_track))
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

		//if every carriage has left the junction, set everything back to normal
		if (carriages_in_junction.Count == 0)
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
