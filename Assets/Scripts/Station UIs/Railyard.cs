using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railyard : stationUI{

	public GameObject carriage_to_remove;//temporary solution

	[HideInInspector]
	public bool dragging;

	public override void SetupUI()
	{

	}

	public void AddCarriage(GameObject added_carriage)
	{
		train.AddCarriage(added_carriage);  //eventually add some logic for removing a specific carriage
	}

	public void RemoveCarriage(GameObject selected_carriage)
	{
		//GameObject rear_train = train.RemoveCarriage(selected_carriage);
		GameObject rear_train = train.RemoveCarriage(carriage_to_remove);//temp, replace with above line
		train.CombineTrains(rear_train.GetComponent<TrainController>());
	}



}
