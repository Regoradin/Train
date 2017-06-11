using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railyard : stationUI{

	public GameObject added_carriage;

	public override void SetupUI()
	{

	}

	public void AddCarriage()
	{
		train.AddCarriage(added_carriage);  //eventually add some logic for removing a specific carriage
	}

	public void RemoveCarriage()
	{
		GameObject selected_carriage = added_carriage; //make this eventually actually pick a carriage
		GameObject rear_train = train.RemoveCarriage(selected_carriage);
		train.CombineTrains(rear_train.GetComponent<TrainController>());
		
	}

}
