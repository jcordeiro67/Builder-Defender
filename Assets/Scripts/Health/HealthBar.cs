using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	[SerializeField] private HealthSystem healthSystem; //HealthSystem on Building parent gameObject
	private Transform barTransform; //The healthBar transform reference

	private void Awake ()
	{
		barTransform = transform.Find ("bar"); //set the reference to the healthBar
	}

	private void Start ()
	{
		//update healbar
		UpdateHealthBar ();
		UpdateHealthBarVisibility ();
		//subscribe to healthSystem OnDamage and onHealed events
		healthSystem.OnDamage += HealthSystem_OnDamage;
		healthSystem.OnHealed += HealthSystem_OnHealed;
	}

	private void HealthSystem_OnHealed (object sender, System.EventArgs e)
	{
		UpdateHealthBar ();
		UpdateHealthBarVisibility ();
	}

	private void HealthSystem_OnDamage (object sender, System.EventArgs e)
	{
		//Update healthbar when event OnDamage is called
		UpdateHealthBar ();
		UpdateHealthBarVisibility ();
	}

	private void UpdateHealthBar ()
	{
		//Sets localscale of transform to normalized health amount
		barTransform.localScale = new Vector3 (healthSystem.GetHealthAmountNormalized (), 1, 1);
	}

	private void UpdateHealthBarVisibility ()
	{
		if (healthSystem.IsFullHealth ()) {
			gameObject.SetActive (false);
		} else {
			gameObject.SetActive (true);
		}
	}

}
