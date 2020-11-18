using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishUI : MonoBehaviour {

	[SerializeField] private Building building;
	private Button button;


	private void Awake ()
	{
		button = transform.Find ("button").GetComponent<Button> ();
		button.onClick.AddListener (() => {
			BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder> ().buildingType;

			//Get BuildingType return rate
			float demolishResourceReturnRate = buildingType.demolishResourceReturnRate;

			//Return Each resource based on resourceReturnRate
			foreach (ResourceAmount resourceAmount in buildingType.constructionResourceCostArray) {
				ResourceManager.Instance.AddResource (resourceAmount.resourceType,
					Mathf.FloorToInt (resourceAmount.amount * demolishResourceReturnRate));

			}
			DestroyBuilding (building.gameObject);
		});
	}

	private void DestroyBuilding (GameObject buildingGameObject)
	{
		Destroy (buildingGameObject);
	}
}
