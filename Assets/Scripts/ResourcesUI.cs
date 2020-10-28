﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour {

	[SerializeField] private Transform resourceTemplate;
	private ResourceTypeListSO resourceTypeList;
	private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;

	private void Awake ()
	{
		resourceTypeList = Resources.Load<ResourceTypeListSO> (typeof (ResourceTypeListSO).Name);
		resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform> ();
		resourceTemplate.gameObject.SetActive (false);

		int index = 0;
		foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
			Transform resourceTransform = Instantiate (resourceTemplate, transform);
			resourceTransform.gameObject.SetActive (true);

			float offsetAmount = -150f;

			resourceTransform.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (offsetAmount * index, 0);
			resourceTransform.Find ("image").GetComponent<Image> ().sprite = resourceType.sprite;

			resourceTypeTransformDictionary [resourceType] = resourceTransform;
			index++;
		}
	}

	private void Start ()
	{
		ResourceManager.Instance.OnResourceChange += ResourceManager_OnResourceAmountChanged;
		UpdateResourceAmount ();
	}

	private void ResourceManager_OnResourceAmountChanged (object sender, System.EventArgs eventArgs)
	{
		UpdateResourceAmount ();
	}

	private void UpdateResourceAmount ()
	{
		foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
			Transform resourceTransform = resourceTypeTransformDictionary [resourceType];
			int resourceAmount = ResourceManager.Instance.GetResourceAmount (resourceType);
			resourceTransform.Find ("text").GetComponent<TextMeshProUGUI> ().SetText (resourceAmount.ToString ());
		}

	}
}
