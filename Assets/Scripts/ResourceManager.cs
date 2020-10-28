using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

	public static ResourceManager Instance { get; private set; }


	private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

	private void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}

		//Initilize List of Resource Types
		resourceAmountDictionary = new Dictionary<ResourceTypeSO, int> ();
		//Populate List with Scriptable Objects from the Scriptable Object List
		ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO> (typeof (ResourceTypeListSO).Name);
		//Set default amount for each item in list
		foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
			resourceAmountDictionary [resourceType] = 0;
		}

		TestResourceAmountDictionary ();
	}

	private void Update ()
	{
		if (Input.GetKeyDown (KeyCode.T)) {
			ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO> (typeof (ResourceTypeListSO).Name);
			AddResource (resourceTypeList.list [0], 2);
			TestResourceAmountDictionary ();
		}
	}

	private void TestResourceAmountDictionary ()
	{
		foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys) {
			Debug.Log (resourceType.nameString + "; " + resourceAmountDictionary [resourceType]);
		}
	}

	public void AddResource (ResourceTypeSO resourceType, int amount)
	{
		resourceAmountDictionary [resourceType] += amount;
		TestResourceAmountDictionary ();
	}
}
