using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : MonoBehaviour {

	private float shootTimer;
	//[SerializeField] private float shootTimerMax;
	//[SerializeField] private float targetMaxRadius = 20f;
	private float shootTimerMax;
	private float targetMaxRadius;
	private Enemy targetEnemy;
	private float lookForTargetTimer;
	private readonly float lookForTargetTimerMax = .2f;
	private Transform projectileSpawnTransform;
	private BuildingTypeHolder building;

	private void Awake ()
	{
		projectileSpawnTransform = transform.Find ("projectileSpawnPosition");
	}

	private void Start ()
	{
		building = GetComponent<BuildingTypeHolder> ();
		shootTimerMax = building.buildingType.shootTimerMax;
		targetMaxRadius = building.buildingType.targetMaxRadius;
	}

	private void Update ()
	{
		HandleTargeting ();
		HandleShooting ();
	}

	private void HandleShooting ()
	{
		shootTimer -= Time.deltaTime;
		if (shootTimer <= 0f) {
			shootTimer += shootTimerMax;
			if (targetEnemy != null) {
				ArrowProjectile.Create (projectileSpawnTransform.position, targetEnemy);
			}
		}

	}

	private void LookForTarget ()
	{

		Collider2D [] collider2DArray = Physics2D.OverlapCircleAll (transform.position, targetMaxRadius);

		foreach (Collider2D collider2D in collider2DArray) {
			Enemy enemy = collider2D.GetComponent<Enemy> ();
			if (enemy != null) {
				//Is a enemy
				if (targetEnemy == null) {
					targetEnemy = enemy;
				} else {
					if (Vector3.Distance (transform.position, enemy.transform.position)
						< Vector3.Distance (transform.position, targetEnemy.transform.position)) {
						//switch target to closer enemy
						targetEnemy = enemy;
					}
				}
			}
		}
	}

	private void HandleTargeting ()
	{
		lookForTargetTimer -= Time.deltaTime;
		if (lookForTargetTimer < 0f) {
			lookForTargetTimer += lookForTargetTimerMax;
			LookForTarget ();
		}
	}
}
