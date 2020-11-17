using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

	[SerializeField] private GameObject spriteGameObject;
	private ResourceNearbyOverlay resourceNearbyOverlay;
	private void Awake ()
	{
		resourceNearbyOverlay = transform.Find ("PFResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay> ();
		resourceNearbyOverlay.Hide ();
		Hide ();
	}

	private void Start ()
	{
		BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChanged;
	}

	private void BuildingManager_OnActiveBuildingTypeChanged (object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
	{
		if (e.activeBuildingType == null) {
			Hide ();
			resourceNearbyOverlay.Hide ();
		} else {
			Show (e.activeBuildingType.sprite);

			//Check if building has resourceData and display resourceOverlay on ghost
			if (e.activeBuildingType.hasResourceData) {
				//does have resourceData
				resourceNearbyOverlay.Show (e.activeBuildingType.resourceGeneratorData);
			} else {
				//does not contain resourceData
				resourceNearbyOverlay.Hide ();
			}
		}
	}

	private void Update ()
	{
		transform.position = UtilsClass.GetMouseWorldPosition ();
	}

	private void Show (Sprite ghostSprite)
	{
		spriteGameObject.SetActive (true);
		spriteGameObject.GetComponent<SpriteRenderer> ().sprite = ghostSprite;
	}

	private void Hide ()
	{
		spriteGameObject.SetActive (false);
	}
}
