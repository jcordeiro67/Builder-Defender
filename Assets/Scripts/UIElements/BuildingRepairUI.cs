using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairUI : MonoBehaviour {


	[SerializeField] private Building building;
	[SerializeField] private HealthSystem healthSystem;
	private Button button;

	private void Awake ()
	{
		button = transform.Find ("button").GetComponent<Button> ();
		button.onClick.AddListener (() => {
			//Reset building to max health or a percentage of max health
			healthSystem.HealFull ();

		});
	}
}
