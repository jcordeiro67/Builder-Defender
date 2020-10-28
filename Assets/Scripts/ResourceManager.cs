﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

	public static ResourceManager Instance { get; private set; } //Instance used for singleton
	public event EventHandler OnResourceChange; //Event for when resource amout changes
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

	}

	private void Update ()
	{

	}

	//private void TestResourceAmountDictionary ()
	//{
	//	//Displays the resource amount for each resourceType
	//	foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys) {
	//		Debug.Log (resourceType.nameString + "; " + resourceAmountDictionary [resourceType]);
	//	}
	//}

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
}
