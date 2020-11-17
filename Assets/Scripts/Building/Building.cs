using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	private HealthSystem healthSystem; //The healthSystem reference
	private BuildingTypeSO buildingType; //The buildingType scriptable object reference

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
}
