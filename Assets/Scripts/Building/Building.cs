using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	private HealthSystem healthSystem; //The healthSystem reference
	private BuildingTypeSO buildingType; //The buildingType scriptable object reference
	private Transform buildingDemolishBtn;


	private void Awake ()
	{
		buildingDemolishBtn = transform.Find ("PFBuildingDemolishUI");
		if (buildingDemolishBtn != null) {
			buildingDemolishBtn.gameObject.SetActive (false);
		}
	}
	private void Start ()
	{
		buildingType = GetComponent<BuildingTypeHolder> ().buildingType; //Set reference to the buildingType scriptable object
		healthSystem = GetComponent<HealthSystem> (); //Set reference to healthSystem
		healthSystem.SetHealthAmountMax (buildingType.healthAmountMax, true);
		healthSystem.OnDie += HealthSystem_OnDie;   //subscribe to event OnDie
	}

	private void HealthSystem_OnDie (object sender, System.EventArgs e)
	{
		//called when OnDie event is called
		Destroy (gameObject);
	}

	private void OnMouseEnter ()
	{
		ShowDemolishBtn ();
	}

	private void OnMouseExit ()
	{
		HideDemolishBtn ();
	}

	private void ShowDemolishBtn ()
	{
		if (buildingDemolishBtn != null) {
			buildingDemolishBtn.gameObject.SetActive (true);
		}
	}

	private void HideDemolishBtn ()
	{
		if (buildingDemolishBtn != null) {
			buildingDemolishBtn.gameObject.SetActive (false);
		}
	}
}
