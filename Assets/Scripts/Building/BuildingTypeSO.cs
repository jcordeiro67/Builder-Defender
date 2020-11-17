using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObjects/BuildingType")]

public class BuildingTypeSO : ScriptableObject {
	public string nameString;
	public Transform prefab;
	public bool hasResourceData;
	public ResourceGeneratorData resourceGeneratorData;
	//TODO: implement multiple resource types
	//public List<ResourceGeneratorData> resourceGeneratorData;
	public Sprite sprite;
	public float minConstructionRadius;
	public ResourceAmount [] constructionResourceCostArray;
	public int healthAmountMax;
	public float constructionTimerMax;
	public float shootTimerMax;
	public float targetMaxRadius;

	public string GetConstructionResourceCostString ()
	{
		//Creates the string for the tooltip text
		string str = "";

		foreach (ResourceAmount resourceAmount in constructionResourceCostArray) {
			str += "<color=#" + resourceAmount.resourceType.colorHex + ">" +
				resourceAmount.resourceType.nameShort + resourceAmount.amount + "</color> ";
		}

		return str;
	}


}
