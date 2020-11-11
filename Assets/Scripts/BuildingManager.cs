using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour {

	public static BuildingManager Instance { get; private set; }

	public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChange;

	public class OnActiveBuildingTypeChangedEventArgs : EventArgs {
		public BuildingTypeSO activeBuildingType;
	}

	public float maxConstructionRadius = 15f;
	private BuildingTypeSO activeBuildingType;
	private BuildingTypeListSO buildingTypeList;

	private void Awake ()
	{
		Instance = this;

		//Load the building types from the building type list scriptable Object
		buildingTypeList = (Resources.Load<BuildingTypeListSO> (typeof (BuildingTypeListSO).Name));

	}
	private void Start ()
	{

	}

	private void Update ()
	{
		//Place the building type prefab when mouse is clicked
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ()) {
			if (activeBuildingType != null && CanSpawnBuilding (activeBuildingType, UtilsClass.GetMouseWorldPosition ())) {
				Instantiate (activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition (), Quaternion.identity);
			}
		}

	}

	public void SetActiveBuildingType (BuildingTypeSO buildingType)
	{
		activeBuildingType = buildingType;

		OnActiveBuildingTypeChange?.Invoke (this,
			new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
	}

	public BuildingTypeSO GetActiveBuildingType ()
	{
		return activeBuildingType;
	}

	private bool CanSpawnBuilding (BuildingTypeSO buildingType, Vector3 position)
	{
		//Building Placement rules
		BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D> ();
		Collider2D [] colliders2DArray = Physics2D.OverlapBoxAll (position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
		bool isAreaClear = colliders2DArray.Length == 0;
		if (!isAreaClear) {
			return false;
		}

		//Check minimum Distance to a building of same type
		colliders2DArray = Physics2D.OverlapCircleAll (position, buildingType.minConstructionRadius);

		foreach (Collider2D collider2D in colliders2DArray) {
			//Colliders inside contruction radius
			BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder> ();
			if (buildingTypeHolder != null) {
				//Has buildingTypeHolder
				if (buildingTypeHolder.buildingType == buildingType) {
					// have building of same type in construction radius
					return false;
				}
			}
		}

		//Check Maximum Distance from same building type
		colliders2DArray = Physics2D.OverlapCircleAll (position, maxConstructionRadius);

		foreach (Collider2D collider2D in colliders2DArray) {
			//Colliders inside contruction radius
			BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder> ();
			if (buildingTypeHolder != null) {
				//it's a building
				return true;
			}
		}
		return false;
	}
}
