using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Railyard : stationUI{

	public GameObject carriage_to_remove;//temporary solution

	private List<GameObject> garage; //all of the carriages that are in the garage

	//stuff for managing the drag-and-drop
	public GameObject dropspot;
	private RectTransform rt;

	[HideInInspector]
	public bool dragging;

	void Start()
	{
		rt = dropspot.transform as RectTransform;

		garage = new List<GameObject>();
	}

	public override void SetupUI()
	{
		//put the buttons where they go, do all the things.
	}

	public void Drop(PointerEventData data)
	{
		Vector2 mouse_canvas_position;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, data.position, null, out mouse_canvas_position);

		if (rt.rect.Contains(mouse_canvas_position))
		{
			train.AddCarriage(carriage_to_remove);
			Destroy(data.pointerDrag);
		}
		else
		{
			data.pointerDrag.GetComponent<Draggable>().Reset();
		}
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
