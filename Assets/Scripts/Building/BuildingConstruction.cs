﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour {

	public static BuildingConstruction Create (Vector3 position, BuildingTypeSO buildingType)
	{
		Transform pfBuildingConstruction = Resources.Load<Transform> ("PFBuildingConstruction");
		Transform buildingConstructionTransform = Instantiate (pfBuildingConstruction, position, Quaternion.identity);

		BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction> ();
		buildingConstruction.SetBuildingType (buildingType);
		return buildingConstruction;
	}

	private BuildingTypeSO buildingType;
	private float constructionTimer;
	private float constructionTimerMax;
	private BoxCollider2D boxCollider2D;
	private SpriteRenderer spriteRenderer;
	private BuildingTypeHolder buildingTypeHolder;
	private Material constructionMaterial;

	private void Awake ()
	{
		boxCollider2D = GetComponent<BoxCollider2D> ();
		spriteRenderer = transform.Find ("sprite").GetComponent<SpriteRenderer> ();
		buildingTypeHolder = GetComponent<BuildingTypeHolder> ();
		constructionMaterial = spriteRenderer.material;
	}

	private void Update ()
	{
		constructionTimer -= Time.deltaTime;
		constructionMaterial.SetFloat ("_Progress", GetConstructionTimerNormalized ());
		if (constructionTimer < 0) {
			Instantiate (buildingType.prefab, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

	private void SetBuildingType (BuildingTypeSO buildingType)
	{
		this.buildingType = buildingType;
		spriteRenderer.sprite = buildingType.sprite;
		constructionTimerMax = buildingType.constructionTimerMax;
		constructionTimer = constructionTimerMax;

		//Dynamicaly set size of boxCollider2D on buildingConstruction prefab;
		boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D> ().offset;
		boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D> ().size;
		//Set BuildingType on ConstructionProgress Prefab
		buildingTypeHolder.buildingType = buildingType;
	}

	public float GetConstructionTimerNormalized ()
	{
		return 1 - constructionTimer / constructionTimerMax;
	}
}
