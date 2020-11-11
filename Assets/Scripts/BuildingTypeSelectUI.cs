using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour {

	[SerializeField] private Sprite cursorSprite;
	private Transform cursorBtn;
	[SerializeField] private Transform btnTemplate;
	[SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
	private Dictionary<BuildingTypeSO, Transform> btnTransformDictionary;

	private void Awake ()
	{
		btnTemplate.gameObject.SetActive (false);

		//Load the building types from the building type list scriptable Object
		BuildingTypeListSO buildingTypeList = (Resources.Load<BuildingTypeListSO> (typeof (BuildingTypeListSO).Name));

		btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform> ();

		float index = 0;

		cursorBtn = Instantiate (btnTemplate, transform);
		cursorBtn.gameObject.SetActive (true);

		//Move button
		float offsetAmount = +140f;
		cursorBtn.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (offsetAmount * index, 0);
		cursorBtn.Find ("image").GetComponent<Image> ().sprite = cursorSprite;
		cursorBtn.Find ("image").GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, -40);

		//Setup button listener function
		cursorBtn.GetComponent<Button> ().onClick.AddListener (() => {
			BuildingManager.Instance.SetActiveBuildingType (null);
		});

		index++;

		//Create a button for each buildingTypeSO
		foreach (BuildingTypeSO buildingType in buildingTypeList.list) {
			if (ignoreBuildingTypeList.Contains (buildingType)) {
				continue;
			}
			Transform btnTransform = Instantiate (btnTemplate, transform);
			btnTransform.gameObject.SetActive (true);

			//Move button
			offsetAmount = +140f;
			btnTransform.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (offsetAmount * index, 0);
			btnTransform.Find ("image").GetComponent<Image> ().sprite = buildingType.sprite;

			//Setup button listener function
			btnTransform.GetComponent<Button> ().onClick.AddListener (() => {
				BuildingManager.Instance.SetActiveBuildingType (buildingType);
			});

			btnTransformDictionary [buildingType] = btnTransform;

			index++;
		}
	}

	private void Start ()
	{
		BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChanged;
		UpdateActiveBuildingTypeButton ();
	}

	private void BuildingManager_OnActiveBuildingTypeChanged (object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
	{
		UpdateActiveBuildingTypeButton ();
	}

	private void UpdateActiveBuildingTypeButton ()
	{
		cursorBtn.Find ("selected").gameObject.SetActive (false);
		//Adds a active indicator to selected buildingType
		foreach (BuildingTypeSO buildingType in btnTransformDictionary.Keys) {
			Transform btntransform = btnTransformDictionary [buildingType];
			btntransform.Find ("selected").gameObject.SetActive (false);
		}

		BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType ();
		if (activeBuildingType == null) {
			cursorBtn.Find ("selected").gameObject.SetActive (true);
		} else {
			btnTransformDictionary [activeBuildingType].Find ("selected").gameObject.SetActive (true);
		}
	}

}
