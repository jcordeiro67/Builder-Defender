using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairUI : MonoBehaviour {


	[SerializeField] private Building building;
	[SerializeField] private HealthSystem healthSystem;
	[SerializeField] private ResourceTypeSO goldResourceType;
	private Button button;

	private void Awake ()
	{
		button = transform.Find ("button").GetComponent<Button> ();
		button.onClick.AddListener (() => {
			//Reset building to max health or a percentage of max health
			int missingHealth = healthSystem.GetHealthAmountMax () - healthSystem.GetHealthAmount ();
			int repairCost = missingHealth / 2;

			ResourceAmount [] resourceCost = new ResourceAmount [] {
				new ResourceAmount { resourceType = goldResourceType, amount = repairCost } };

			if (ResourceManager.Instance.CanAfford (resourceCost)) {
				//Can afford Repair
				ResourceManager.Instance.SpendResources (resourceCost);
				healthSystem.HealFull ();
			} else {
				//Cant Afford Repair
				TooltipUI.Instance.Show ("Cannot Afford Repair Cost!", new TooltipUI.TooltipTimer { timer = 2f });
			}

		});
	}
}
