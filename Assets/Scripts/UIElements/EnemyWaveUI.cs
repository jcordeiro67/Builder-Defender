﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour {
	/// <summary>
	///Handles the enemyWaveText and nextWaveSpawnTimer display in the EnemyWaveUI.
	//Handles the visibility, position and rotation of the wave spawn indicator
	//and the closest enemy indicator
	//Requires EnemyWaveManager
	/// </summary>

	[SerializeField] EnemyWaveManager enemyWaveManager;
	private Camera mainCamera;
	private TextMeshProUGUI enemyWaveText;
	private TextMeshProUGUI waveMessageText;
	private RectTransform enemyWaveSpawnIndicator;
	private RectTransform enemyClosestPositionIndicator;

	private void Awake ()
	{
		enemyWaveText = transform.Find ("waveNumberText").GetComponent<TextMeshProUGUI> ();
		waveMessageText = transform.Find ("waveMessageText").GetComponent<TextMeshProUGUI> ();
		enemyWaveSpawnIndicator = transform.Find ("enemyWaveSpawnIndicator").GetComponent<RectTransform> ();
		enemyClosestPositionIndicator = transform.Find ("enemyClosestPositionIndicator").GetComponent<RectTransform> ();
	}
	private void Start ()
	{
		mainCamera = Camera.main;
		enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
		SetWaveNumberText ("Wave " + enemyWaveManager.GetWaveNumber ().ToString ());
	}

	private void EnemyWaveManager_OnWaveNumberChanged (object sender, System.EventArgs e)
	{
		//Called when EnemyWaveManager.OnWaveNumberChanged event called
		//Sets the waveNumber text in the UI
		SetWaveNumberText ("Wave " + enemyWaveManager.GetWaveNumber ().ToString ());

		//TODO: Add Text to the EnemyWaveUI to display the total number of enemies
		//spawned with each wave
	}

	private void Update ()
	{
		HandleNextWaveMessage ();
		HandleEnemyWaveSpawnIndicator ();
		HandleEnemyClosestPositionIndicator ();
	}

	private void HandleNextWaveMessage ()
	{
		//Sets the nextwave message timer in the UI
		float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer ();
		if (nextWaveSpawnTimer <= 0f) {
			SetMessageText ("");
		} else {
			SetMessageText ("Next Wave in " + nextWaveSpawnTimer.ToString ("F1") + "s");
		}
	}

	private void HandleEnemyWaveSpawnIndicator ()
	{
		//Enemy Wave Indicator: Sets the position and rotation of the nextWaveIndicator

		//Get the position normalized between the spwanPosition and the mainCamera
		Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition () - mainCamera.transform.position).normalized;

		//Set the spawnPositionIndicator to the nextspawn position multiplied by the offset
		enemyWaveSpawnIndicator.anchoredPosition = dirToNextSpawnPosition * 350f;

		//Set the rotation of the indicator to the nextSpawn position
		enemyWaveSpawnIndicator.eulerAngles = new Vector3 (0, 0, UtilsClass.GetAngleFromVector (dirToNextSpawnPosition));

		//Get the distance between the nextSpawn position and main camera
		float distanceToNextSpawnPosition = Vector3.Distance (enemyWaveManager.GetSpawnPosition (), mainCamera.transform.position);

		//Set the indicator visibility acording to main camera ortho size
		enemyWaveSpawnIndicator.gameObject.SetActive (distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5f);
	}

	private void HandleEnemyClosestPositionIndicator ()
	{
		//Find closest target
		float targetMaxRadius = 9999f;
		Collider2D [] collider2DArray = Physics2D.OverlapCircleAll (mainCamera.transform.position, targetMaxRadius);

		Enemy targetEnemy = null;
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

		if (targetEnemy != null) {

			//Closest Enemy Indicator
			Vector3 dirToClosestEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;
			enemyClosestPositionIndicator.anchoredPosition = dirToClosestEnemy * 300f;
			enemyClosestPositionIndicator.eulerAngles = new Vector3 (0, 0, UtilsClass.GetAngleFromVector (dirToClosestEnemy));

			float distanceToClosestEnemy = Vector3.Distance (targetEnemy.transform.position, mainCamera.transform.position);
			enemyClosestPositionIndicator.gameObject.SetActive (distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
		} else {
			//no enemies alive
			enemyClosestPositionIndicator.gameObject.SetActive (false);
		}
	}

	private void SetMessageText (string message)
	{
		waveMessageText.SetText (message);
	}

	private void SetWaveNumberText (string text)
	{
		enemyWaveText.SetText (text);
	}
}
