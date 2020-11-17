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
	[SerializeField] private Building hqBuilding;
	public float maxConstructionRadius = 15f;
	[SerializeField] private float toolTipDelay = 2f;
	private BuildingTypeSO activeBuildingType;
	private BuildingTypeListSO buildingTypeList;

	private void Awake ()
	{
		Instance = this;

		//Load the building types from the building type list scriptable Object
		buildingTypeList = Resources.Load<BuildingTypeListSO> (typeof (BuildingTypeListSO).Name);

	}
	private void Start ()
	{

	}

	private void Update ()
	{
		//Place the building type prefab when mouse is clicked
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ()) {

			if (activeBuildingType != null) {
				if (CanSpawnBuilding (activeBuildingType, UtilsClass.GetMouseWorldPosition (), out string errorMessage)) {
					if (ResourceManager.Instance.CanAfford (activeBuildingType.constructionResourceCostArray)) {
						ResourceManager.Instance.SpendResources (activeBuildingType.constructionResourceCostArray);
						//Instantiate (activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition (), Quaternion.identity);
						BuildingConstruction.Create (UtilsClass.GetMouseWorldPosition (), activeBuildingType);
					} else {
						TooltipUI.Instance.Show ("Cannot afford " + activeBuildingType.GetConstructionResourceCostString (),
							new TooltipUI.TooltipTimer { timer = toolTipDelay });
					}
				} else {
					TooltipUI.Instance.Show (errorMessage, new TooltipUI.TooltipTimer { timer = toolTipDelay });
				}
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

	private bool CanSpawnBuilding (BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
	{
		//Building Placement rules
		//Check if area is clear 
		BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D> ();
		Collider2D [] colliders2DArray = Physics2D.OverlapBoxAll (position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
		bool isAreaClear = colliders2DArray.Length == 0;
		if (!isAreaClear) {
			errorMessage = "Area Not Clear";
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
					errorMessage = "Too close to another building of the same type";
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
				errorMessage = "";
				return true;
			}
		}
		//too far from any buildings
		errorMessage = "To far away from any other building";
		return false;
	}

	public Building GetHQBuilding ()
	{
		return hqBuilding;
	}
}
