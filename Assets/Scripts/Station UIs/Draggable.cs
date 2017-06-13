using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler{

	RectTransform rt;

	public GameObject carriage;

	private Vector3 original_position;

	private Railyard railyard;

	private void Start()
	{
		railyard = GetComponentInParent<Railyard>();

		rt = transform as RectTransform;

		original_position = rt.position;
	}

	public void OnBeginDrag(PointerEventData data)
	{
		Debug.Log("beigninsdfo dragging");
	}

	public void OnDrag(PointerEventData data)
	{
		rt.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData data)
	{
		//makes the railyard calculate if it is in a drop zone and what needs to happen based on that
		railyard.Drop(data);
	}

	public void Reset()
	{
		rt.position = original_position;
	}
}
