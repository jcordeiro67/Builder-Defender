using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ResourceGenerator_MultipleTypes : MonoBehaviour {

//	//private BuildingTypeSO buildingType;
//	private List<ResourceGeneratorData> generatorDataList;
//	private Dictionary<ResourceGeneratorData, float> timerDict;
//	private Dictionary<ResourceGeneratorData, float> timerMaxDict;

//	private void Awake ()
//	{
//		//buildingType = GetComponent<BuildingTypeHolder> ().buildingType;
//		generatorDataList = GetComponent<BuildingTypeHolder> ().buildingType.resourceGeneratorData;

//		timerDict = new Dictionary<ResourceGeneratorData, float> ();
//		timerMaxDict = new Dictionary<ResourceGeneratorData, float> ();

//		foreach (ResourceGeneratorData resourceGeneratorData in generatorDataList) {
//			timerDict [resourceGeneratorData] = 0f;
//			timerMaxDict [resourceGeneratorData] = resourceGeneratorData.timerMax;
//		}
//	}

//	private void Start ()
//	{
//		int nearbyResourceAmount = 0;
//		foreach (ResourceGeneratorData resourceGeneratorData in generatorDataList) {
//			Collider2D [] collider2DArray = Physics2D.OverlapCircleAll (transform.position, resourceGeneratorData.resourceDetectionRadius);

//			foreach (Collider2D collider2D in collider2DArray) {
//				ResourceNode resourceNode = collider2D.GetComponent<ResourceNode> ();
//				if (resourceNode != null && resourceNode.resourceType == resourceGeneratorData.resourceType) {
//					//It's a respurce node
//					nearbyResourceAmount++;
//				}
//			}
//			nearbyResourceAmount = Mathf.Clamp (nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);

//			Debug.Log ("nearbyResourceAmount: " + nearbyResourceAmount);
//			if (nearbyResourceAmount == 0) {
//				//No resource nodes near by
//				//Disable resource generator
//				enabled = false;
//			} else {
//				timerMaxDict [resourceGeneratorData] = (resourceGeneratorData.timerMax / 2f) +
//					resourceGeneratorData.timerMax *
//					(1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
//			}
//		}
//	}

//	private void Update ()
//	{
//		foreach (ResourceGeneratorData resourceGeneratorData in generatorDataList) {
//			timerDict [resourceGeneratorData] -= Time.deltaTime;
//			if (timerDict [resourceGeneratorData] <= 0f) {
//				timerDict [resourceGeneratorData] += timerMaxDict [resourceGeneratorData];
//				ResourceManager.Instance.AddResource (resourceGeneratorData.resourceType, 1);
//			}
//		}
//	}

//}
