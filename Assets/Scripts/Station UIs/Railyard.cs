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
		draggables.Clear();

		float max_icon_width = 0;
		//put the buttons where they go
		for (int i = 0; i < garage.Count; i++)
		{
			GameObject icon = Instantiate(draggable_icon);

			icon.transform.SetParent(garage_scroll_content.transform);

			//initial position at top of content
			Vector2 new_local_position = new Vector2(0, garage_scroll_content.GetComponent<RectTransform>().anchoredPosition.y);
			//applying all the offsets
			Rect icon_rect = icon.GetComponent<RectTransform>().rect;
			Vector2 position_modifier = new Vector2(- icon_offset - icon_rect.width/2, (icon_offset + icon_rect.height) * i + icon_rect.height/2 + icon_offset);
			new_local_position -= position_modifier;


			icon.GetComponent<RectTransform>().anchorMax = Vector2.up;
			icon.GetComponent<RectTransform>().anchorMin = Vector2.up;
			icon.GetComponent<RectTransform>().anchoredPosition = new_local_position;

			if (icon_rect.width > max_icon_width)
			{
				max_icon_width = icon_rect.width;
			}

			icon.GetComponent<Draggable>().carriage = garage[i];

			draggables.Add(icon);
		}

		//making the scrollview and content the right size
		RectTransform view_rect = garage_scroll_view.GetComponent<RectTransform>();

		float width = icon_offset * 2 + max_icon_width;
		width += garage_scroll_view.GetComponent<ScrollRect>().verticalScrollbar.GetComponent<RectTransform>().rect.width;

		view_rect.sizeDelta = new Vector2(width, view_rect.sizeDelta.y);
		view_rect.anchoredPosition = new Vector2(width / 2, view_rect.anchoredPosition.y);

		float height = icon_offset;
		if (draggables.Count != 0)
		{
			foreach (GameObject icon in draggables)
			{
				height += icon.GetComponent<RectTransform>().rect.height + icon_offset;
			}
		}

		height += garage_scroll_view.GetComponent<ScrollRect>().horizontalScrollbar.GetComponent<RectTransform>().rect.height;

		RectTransform content_rect = garage_scroll_content.GetComponent<RectTransform>();
		content_rect.sizeDelta = new Vector2(content_rect.sizeDelta.x, height);

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
