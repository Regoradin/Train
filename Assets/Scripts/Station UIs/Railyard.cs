using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Railyard : stationUI{

	private GameObject selected_carriage;

	public List<GameObject> garage; //all of the carriages that are in the garage

	public GameObject draggable_icon;
	public GameObject garage_scroll_view;
	private GameObject garage_scroll_content;

	//a list of all generated elements that are generated from SetupUI() so that they can be wiped every time the UI is regenerated.
	private List<GameObject> generated_elems;
	public float icon_offset;
	
	public GameObject dropspot;
	public GameObject train_scroll_view;
	private GameObject train_scroll_content;

	private RectTransform rt;

	[HideInInspector]
	public bool dragging;

	void Start()
	{
		rt = dropspot.transform as RectTransform;

		//garage = new List<GameObject>();
		generated_elems = new List<GameObject>();
	}

	public override void SetupUI()
	{
		//wiping all generated elements
		foreach (GameObject elem in generated_elems)
		{
			Destroy(elem);
		}
		generated_elems.Clear();

		List<GameObject> garage_tiles = GenerateScrollView(garage_scroll_view, draggable_icon, garage.Count);
		for (int i = 0; i < garage.Count; i++)
		{
			garage_tiles[i].GetComponent<Draggable>().carriage = garage[i];
		}

		

		//setting up the train display - This is all based off of the dimensions of dropspot for now

		//creating a prototype button object to be generated from. This may be done by a prefab in the future which would make a lot of this setup unnescessary
		GameObject toggle_object = new GameObject();
		RectTransform button_rt = toggle_object.AddComponent<RectTransform>();
		button_rt.sizeDelta = dropspot.GetComponent<RectTransform>().sizeDelta;
		toggle_object.AddComponent<Toggle>();
		toggle_object.AddComponent<Image>();

		//creating the toggle group that they will all use
		GameObject toggle_group = new GameObject();
		toggle_group.AddComponent<ToggleGroup>();

		List<GameObject> toggles = GenerateScrollView(train_scroll_view, toggle_object, train.Carriages.Count);

		for (int i = 0; i < train.Carriages.Count; i++)
		{
			toggles[i].GetComponent<Image>().sprite = train.Carriages[i].GetComponent<CarriageController>().sprite;
			toggles[i].GetComponent<Toggle>().group = toggle_group.GetComponent<ToggleGroup>();

			//changes the highlighted color so you can see what you have selected
			ColorBlock colors = toggles[i].GetComponent<Toggle>().colors;
			colors.highlightedColor = new Color(1, 1, 1);
			Debug.Log("Chanign color for " + colors);

			//adding functionality. x=i exists so that the value that it sets it to doesn't increment as it goes through the list
			int x = i;
			toggles[i].GetComponent<Toggle>().onValueChanged.AddListener(delegate (bool input)
			{
				if (input == true)
				{
					selected_carriage = train.Carriages[x];
				}
			});
		}

	}

	/// <summary>
	/// Takes a scroll view and a gameobject that is the icon that is spaced out evenly in the columns. Returns a list of all of the icons so that they can be customized with stuff
	/// </summary>
	/// <param name="scroll_view">The ScrollView that this is being done to</param>
	/// <param name="icon">The gameobject that is being instantiated</param>
	/// <param name="icon_count">The number of icons to instantiate</param>
	/// <returns></returns>
	private List<GameObject> GenerateScrollView(GameObject scroll_view, GameObject icon, int icon_count)
	{
		List<GameObject> result = new List<GameObject>();
		GameObject scroll_content = scroll_view.GetComponent<ScrollRect>().content.gameObject;


		float max_icon_width = 0;
		//Instantiates the tiles and places them where they are supposed to go
		for (int i = 0; i < icon_count; i++)
		{

			GameObject tile = Instantiate(icon);

			tile.transform.SetParent(scroll_content.transform);

			//initial position at top of content
			Vector2 new_local_position = new Vector2(0, scroll_content.GetComponent<RectTransform>().anchoredPosition.y);
			//applying all the offsets
			Rect tile_rect = tile.GetComponent<RectTransform>().rect;
			Vector2 position_modifier = new Vector2(-icon_offset - tile_rect.width / 2, (icon_offset + tile_rect.height) * i + tile_rect.height / 2 + icon_offset);
			new_local_position -= position_modifier;


			tile.GetComponent<RectTransform>().anchorMax = Vector2.up;
			tile.GetComponent<RectTransform>().anchorMin = Vector2.up;
			tile.GetComponent<RectTransform>().anchoredPosition = new_local_position;

			if (tile_rect.width > max_icon_width)
			{
				max_icon_width = tile_rect.width;
			}

			//adds the new tile to the generated_elems list (which is maintained for the whole UI) and the result list (which is local to this ScrollView)
			generated_elems.Add(tile);
			result.Add(tile);

		}

		//setting the ScrollRect to the appropriate size
		RectTransform view_rect = scroll_view.GetComponent<RectTransform>();

		float width = icon_offset * 2 + max_icon_width;
		width += scroll_view.GetComponent<ScrollRect>().verticalScrollbar.GetComponent<RectTransform>().rect.width;

		view_rect.sizeDelta = new Vector2(width, view_rect.sizeDelta.y);
		view_rect.anchoredPosition = new Vector2(width / 2, view_rect.anchoredPosition.y);

		float height = icon_offset;
		if (result.Count != 0)
		{
			foreach (GameObject tile in result)
			{
				height += icon.GetComponent<RectTransform>().rect.height + icon_offset;
			}
		}

		height += scroll_view.GetComponent<ScrollRect>().horizontalScrollbar.GetComponent<RectTransform>().rect.height;

		RectTransform content_rect = scroll_content.GetComponent<RectTransform>();
		content_rect.sizeDelta = new Vector2(content_rect.sizeDelta.x, height);



		return result;
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
		train.AddCarriage(added_carriage);
	}

	public void RemoveCarriage(GameObject selected_carriage)
	{
		GameObject rear_train = train.RemoveCarriage(selected_carriage);
		train.CombineTrains(rear_train.GetComponent<TrainController>());
	}
}
