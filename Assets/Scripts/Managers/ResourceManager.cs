using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

	public static ResourceManager Instance { get; private set; } //Instance used for singleton
	public event EventHandler OnResourceChange; //Event for when resource amout changes

	[SerializeField] private List<ResourceAmount> startingResourceAmountList;
	private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;   //Dictionary to hold resourceType amounts



	private void Awake ()
	{
		//Setup Singleton
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

		foreach (ResourceAmount resourceAmount in startingResourceAmountList) {
			AddResource (resourceAmount.resourceType, resourceAmount.amount);
		}

	}

	public void AddResource (ResourceTypeSO resourceType, int amount)
	{
		//Add resources to the resource amount by resource type
		resourceAmountDictionary [resourceType] += amount;

		//Call the OnResourceChange Event
		OnResourceChange?.Invoke (this, EventArgs.Empty);

	}

	public int GetResourceAmount (ResourceTypeSO resourceType)
	{
		//Returns the recourceType amount
		return resourceAmountDictionary [resourceType];
	}

	public bool CanAfford (ResourceAmount [] resourceAmountArray)
	{
		foreach (ResourceAmount resourceAmount in resourceAmountArray) {
			if (GetResourceAmount (resourceAmount.resourceType) >= resourceAmount.amount) {
				//Can Afford Building
			} else {
				//Can Not Afford Building
				return false;
			}
		}

		//Can Afford All
		return true;
	}

	public void SpendResources (ResourceAmount [] resourceAmountArray)
	{
		foreach (ResourceAmount resourceAmount in resourceAmountArray) {
			resourceAmountDictionary [resourceAmount.resourceType] -= resourceAmount.amount;
		}
	}



}
