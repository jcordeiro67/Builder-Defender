using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

	private Camera m_Camera;
	private BuildingTypeSO buildingType;
	private BuildingTypeListSO buildingTypeList;

	private void Awake ()
	{
		//Load the building types from the building type list scriptable Object
		buildingTypeList = (Resources.Load<BuildingTypeListSO> (typeof (BuildingTypeListSO).Name));
		//Set default building type
		buildingType = buildingTypeList.list [0];
	}
	private void Start ()
	{
		m_Camera = Camera.main;

	}

	private void Update ()
	{
		//Place the building type prefab when mouse is clicked
		if (Input.GetMouseButtonDown (0)) {
			Instantiate (buildingType.prefab, GetMouseWorldPosition (), Quaternion.identity);
		}

		//Toggle through building types depending on key pressed
		if (Input.GetKeyDown (KeyCode.T)) {
			buildingType = buildingTypeList.list [0];
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			buildingType = buildingTypeList.list [1];
		}

	}

	private Vector3 GetMouseWorldPosition ()
	{
		//Gets the mouse world position from the camera screen point
		Vector3 mouseWorldPosition = m_Camera.ScreenToWorldPoint (Input.mousePosition);
		//Reset Y axis to 0 for mouse position
		mouseWorldPosition.z = 0f;
		return mouseWorldPosition;
	}
}
