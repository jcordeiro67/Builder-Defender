using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

	public static int GetNearbyResourceAmount (ResourceGeneratorData resourceGeneratorData, Vector3 position)
	{
		Collider2D [] collider2DArray = Physics2D.OverlapCircleAll (position, resourceGeneratorData.resourceDetectionRadius);
		int nearbyResourceAmount = 0;
		foreach (Collider2D collider2D in collider2DArray) {
			ResourceNode resourceNode = collider2D.GetComponent<ResourceNode> ();

			if (resourceNode != null) {
				//It's a respurce node
				if (resourceNode.resourceType == resourceGeneratorData.resourceType) {
					//Same resource type found as building
					nearbyResourceAmount++;
				}

			}
		}

		nearbyResourceAmount = Mathf.Clamp (nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
		return nearbyResourceAmount;
	}

	//private BuildingTypeSO buildingType;
	private ResourceGeneratorData resourceGeneratorData;
	private float timer;
	private float timerMax;

	//For Future Use
	//private Animator anim;
	//private Dictionary<ResourceGeneratorData, float> timerDict;
	//private Dictionary<ResourceGeneratorData, float> timerMaxDict;

	private void Awake ()
	{

		//buildingType = GetComponent<BuildingTypeHolder> ().buildingType; 
		resourceGeneratorData = GetComponent<BuildingTypeHolder> ().buildingType.resourceGeneratorData;

		timerMax = resourceGeneratorData.timerMax;

	}

	private void Start ()
	{
		//Future Use
		//anim = GetComponentInChildren<Animator> ();

		//Find nearby resources

		int nearbyResourceAmount = GetNearbyResourceAmount (resourceGeneratorData, transform.position);
		if (nearbyResourceAmount == 0) {
			//No resource nodes near by
			//Disable resource generator
			enabled = false;
		} else {
			timerMax = (resourceGeneratorData.timerMax / 2f) +
				resourceGeneratorData.timerMax *
				(1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
		}

		//Debug.Log ("nearbyResourceAmount: " + nearbyResourceAmount + "; " + timerMax);
	}

	private void Update ()
	{

		timer -= Time.deltaTime;
		if (timer <= 0f) {
			timer += timerMax;
			ResourceManager.Instance.AddResource (resourceGeneratorData.resourceType, 1);
		}

	}

	public ResourceGeneratorData GetResourceGeneratorData ()
	{
		return resourceGeneratorData;
	}

	public float GetTimeNormalized ()
	{
		return timer / timerMax;
	}

	public float GetAmountGeneratedPerSecond ()
	{
		return 1 / timerMax;
	}
}
