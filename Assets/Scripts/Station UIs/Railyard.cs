using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Railyard : stationUI{

	public GameObject carriage_to_remove;//temporary solution

	public List<GameObject> garage; //all of the carriages that are in the garage

	public GameObject draggable_icon;
	public GameObject garage_scroll_view;
	private GameObject garage_scroll_content;

	private List<GameObject> draggables;
	public float icon_offset;

	//stuff for managing the drag-and-drop
	public GameObject dropspot;
	private RectTransform rt;

	[HideInInspector]
	public bool dragging;

	void Start()
	{
		rt = dropspot.transform as RectTransform;

		//garage = new List<GameObject>();
		draggables = new List<GameObject>();
		garage_scroll_content = garage_scroll_view.GetComponent<ScrollRect>().content.gameObject;
		foreach (Transform child in garage_scroll_view.transform)
		{
			if(child.name == "Content")
			{
				garage_scroll_content = child.gameObject;
			}
		}
	}

	public override void SetupUI()
	{
		Debug.Log("setting up railyard");
		//get rid of old buttons
		foreach(GameObject icon in draggables)
		{
			Destroy(icon);
		}

		float max_icon_width = 0;
		//put the buttons where they go
		for (int i = 0; i < garage.Count; i++)
		{
			Debug.Log("making draggable " + i);
			GameObject icon = Instantiate(draggable_icon);

			icon.transform.SetParent(garage_scroll_content.transform);
			//initial position at top of content
			Vector2 new_local_position = new Vector2(0, garage_scroll_content.GetComponent<RectTransform>().anchorMax.y);
			//applying all the offsets
			Rect icon_rect = icon.GetComponent<RectTransform>().rect;
			new_local_position -= new Vector2(- icon_offset - icon_rect.width/2, (icon_offset + icon_rect.height) * i + icon_rect.height/2 + icon_offset);

			icon.transform.localPosition = new_local_position;

			if(icon_rect.width > max_icon_width)
			{
				max_icon_width = icon_rect.width;
			}

			icon.GetComponent<Draggable>().carriage = garage[i];

			draggables.Add(icon);
		}

		//making the scrollview and content the right size
		Rect view_rect = garage_scroll_view.GetComponent<RectTransform>().rect;
		view_rect.Set(view_rect.x, view_rect.y, max_icon_width + icon_offset * 2, view_rect.height);

		float height = icon_offset;
		foreach(GameObject icon in draggables)
		{
			height += icon.GetComponent<RectTransform>().rect.height + icon_offset;
		}
		Rect content_rect = garage_scroll_content.GetComponent<RectTransform>().rect;
		content_rect.Set(content_rect.x, content_rect.y, content_rect.width, height);

	}

	public void Drop(PointerEventData data)
	{
		Vector2 mouse_canvas_position;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, data.position, null, out mouse_canvas_position);

		if (rt.rect.Contains(mouse_canvas_position))
		{
			train.AddCarriage(data.pointerDrag.GetComponent<Draggable>().carriage);
			//removing the added train from the garage
			garage.Remove(data.pointerDrag.GetComponent<Draggable>().carriage);
			//setupUI will handle removing the draggable
			SetupUI();

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
