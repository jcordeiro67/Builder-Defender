using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	private HealthSystem healthSystem; //The healthSystem reference
	private BuildingTypeSO buildingType; //The buildingType scriptable object reference
	private Transform buildingDemolishBtn;
	private Transform buildingRepairBtn;


	private void Awake ()
	{
		buildingDemolishBtn = transform.Find ("PFBuildingDemolishUI");
		buildingRepairBtn = transform.Find ("PFBuildingRepairUI");
		if (buildingDemolishBtn != null) {
			HideDemolishBtn ();
		}

		if (buildingRepairBtn != null) {
			HideRepairBtn ();
		}
	}
	private void Start ()
	{
		buildingType = GetComponent<BuildingTypeHolder> ().buildingType; //Set reference to the buildingType scriptable object
		healthSystem = GetComponent<HealthSystem> (); //Set reference to healthSystem
		healthSystem.SetHealthAmountMax (buildingType.healthAmountMax, true);

		healthSystem.OnDamage += HealthSystem_OnDamage;
		healthSystem.OnHealed += HealthSystem_OnHealed;

		healthSystem.OnDie += HealthSystem_OnDie;   //subscribe to event OnDie
	}

	private void HealthSystem_OnHealed (object sender, System.EventArgs e)
	{
		if (healthSystem.IsFullHealth ()) {
			HideRepairBtn ();
		}
	}

	private void HealthSystem_OnDamage (object sender, System.EventArgs e)
	{
		ShowRepairBtn ();
	}

	private void HealthSystem_OnDie (object sender, System.EventArgs e)
	{
		//called when OnDie event is called
		Destroy (gameObject);
	}

	private void OnMouseEnter ()
	{
		ShowDemolishBtn ();
		//ShowRepairBtn ();
	}

	private void OnMouseExit ()
	{
		HideDemolishBtn ();
		//HideRepairBtn ();
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

	private void ShowRepairBtn ()
	{
		if (buildingRepairBtn != null) {
			buildingRepairBtn.gameObject.SetActive (true);
		}
	}

	private void HideRepairBtn ()
	{
		if (buildingRepairBtn != null) {
			buildingRepairBtn.gameObject.SetActive (false);
		}
	}

}
