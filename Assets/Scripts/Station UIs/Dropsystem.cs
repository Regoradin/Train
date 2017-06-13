using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dropsystem : MonoBehaviour {

	public GameObject dropspot;
	private RectTransform rt;

	void Start()
	{
		rt = dropspot.transform as RectTransform;
		Debug.Log(rt.rect);
	}

	public void Drop(PointerEventData data)
	{
		Vector2 mouse_canvas_position;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, data.position, null, out mouse_canvas_position);

		if (rt.rect.Contains(mouse_canvas_position))
		{
			Debug.Log("dropped in the green box");
		}

		data.pointerDrag.GetComponent<Draggable>().Reset();
	}

}
