using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour {

	private TrainController train_controller;

	public string initial_direction;
	
	// Use this for initialization
	void Start () {
		train_controller = GetComponentInParent<TrainController>();

		train_controller.Direction = initial_direction;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
