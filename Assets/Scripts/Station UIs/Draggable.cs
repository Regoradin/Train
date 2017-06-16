using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler{

	private RectTransform rt;

	[HideInInspector]
	public GameObject carriage;

	private Vector3 original_position;

	private Railyard railyard;

	private Transform original_parent;

	private void Start()
	{
		railyard = GetComponentInParent<Railyard>();

		rt = transform as RectTransform;

		original_position = rt.localPosition;
		original_parent = transform.parent;
		GetComponent<Image>().sprite = carriage.GetComponent<CarriageController>().sprite;
	}

	public void OnBeginDrag(PointerEventData data)
	{
		transform.SetParent(railyard.UI.transform);
		transform.SetAsLastSibling();
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
		transform.SetParent(original_parent);
		rt.localPosition = original_position;
	}
}
