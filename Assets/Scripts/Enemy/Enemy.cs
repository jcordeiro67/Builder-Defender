using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public static Enemy Create (Vector3 position)
	{
		Transform pfEnemy = Resources.Load<Transform> ("PFEnemy");
		Transform enemyTransform = Instantiate (pfEnemy, position, Quaternion.identity);

		Enemy enemy = enemyTransform.GetComponent<Enemy> ();
		return enemy;
	}

	private Transform targetTransform;
	private Rigidbody2D m_rigidBody2D;
	private HealthSystem healthSystem;
	private float lookForTargetTimer;
	private readonly float lookForTargetTimerMax = .2f;

	// Start is called before the first frame update
	private void Start ()
	{
		m_rigidBody2D = GetComponent<Rigidbody2D> ();
		if (BuildingManager.Instance.GetHQBuilding () != null) {
			targetTransform = BuildingManager.Instance.GetHQBuilding ().transform;
		}
		lookForTargetTimer = Random.Range (0f, lookForTargetTimerMax);
		healthSystem = GetComponent<HealthSystem> ();

		healthSystem.OnDie += HealthSystem_OnDie;
	}


	// Update is called once per frame
	private void Update ()
	{
		HandleMovement ();
		HandleTargeting ();
	}

	private void HealthSystem_OnDie (object sender, System.EventArgs e)
	{
		Destroy (gameObject);
	}


	private void HandleTargeting ()
	{
		lookForTargetTimer -= Time.deltaTime;
		if (lookForTargetTimer < 0f) {
			lookForTargetTimer += lookForTargetTimerMax;
			LookForTarget ();
		}
	}

	private void HandleMovement ()
	{
		if (targetTransform != null) {
			Vector3 moveDir = (targetTransform.position - transform.position).normalized;

			float moveSpeed = 4f;
			m_rigidBody2D.velocity = moveDir * moveSpeed;
		} else {
			m_rigidBody2D.velocity = Vector2.zero;
		}
	}

	private void OnCollisionEnter2D (Collision2D collision)
	{
		Building building = collision.gameObject.GetComponent<Building> ();

		if (building != null) {
			//Collided with building
			HealthSystem healthSystem = building.GetComponent<HealthSystem> ();
			healthSystem.Damage (10);

			DestroyEnemy ();
		}
	}

	private void LookForTarget ()
	{
		float targetMaxRadius = 10f;
		Collider2D [] collider2DArray = Physics2D.OverlapCircleAll (transform.position, targetMaxRadius);

		foreach (Collider2D collider2D in collider2DArray) {
			Building building = collider2D.GetComponent<Building> ();
			if (building != null) {
				//Is a building
				if (targetTransform == null) {
					targetTransform = building.transform;
				} else {
					if (Vector3.Distance (transform.position, building.transform.position)
						< Vector3.Distance (transform.position, targetTransform.position)) {
						//switch target to closer building
						targetTransform = building.transform;
					}
				}
			}
		}
		if (targetTransform == null) {
			//found no targets within range, Set target to HQ building
			if (BuildingManager.Instance.GetHQBuilding () != null) {
				targetTransform = BuildingManager.Instance.GetHQBuilding ().transform;
			}
		}
	}

	private void DestroyEnemy ()
	{
		Destroy (gameObject);
	}
}
