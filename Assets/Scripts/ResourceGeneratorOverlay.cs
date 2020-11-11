using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour {

	[SerializeField] private ResourceGenerator resourceGenerator;
	private Transform barTransform;

	private void Start ()
	{
		ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData ();

		barTransform = transform.Find ("bar");

		transform.Find ("icon").GetComponent<SpriteRenderer> ().sprite = resourceGeneratorData.resourceType.sprite;
		transform.Find ("bar").localScale = new Vector3 (resourceGenerator.GetTimeNormalized (), 1, 1);
		transform.Find ("text (TMP)").GetComponent<TextMeshPro> ().text = resourceGenerator.GetAmountGeneratedPerSecond ().ToString ("F1");
	}

	private void Update ()
	{
		barTransform.localScale = new Vector3 (1 - resourceGenerator.GetTimeNormalized (), 1, 1);
	}
}
