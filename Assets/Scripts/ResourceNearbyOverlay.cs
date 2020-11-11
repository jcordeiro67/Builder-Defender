using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour {


	private ResourceGeneratorData resourceGeneratorData;

	private void Awake ()
	{
		Hide ();
	}

	private void Update ()
	{
		int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount (resourceGeneratorData, transform.position);
		float percent = Mathf.RoundToInt ((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);
		transform.Find ("text (TMP)").GetComponent<TextMeshPro> ().SetText (percent + "%");
	}

	// Start is called before the first frame update
	public void Show (ResourceGeneratorData resourceGeneratorData)
	{
		this.resourceGeneratorData = resourceGeneratorData;
		gameObject.SetActive (true);
		transform.Find ("icon").GetComponent<SpriteRenderer> ().sprite = resourceGeneratorData.resourceType.sprite;

	}

	// Update is called once per frame
	public void Hide ()
	{
		gameObject.SetActive (false);
	}
}
