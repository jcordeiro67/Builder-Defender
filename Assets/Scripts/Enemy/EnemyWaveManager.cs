using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyWaveManager : MonoBehaviour {


	public event EventHandler OnWaveNumberChanged;

	private enum State {
		WaitingToSpawnNextWave,
		SpawningWave,
	}

	[SerializeField] private int enemySpawnAmount;
	[SerializeField] private float enemySpawnDelay;
	[SerializeField] private int enemyIncreasePerWave;
	[SerializeField] private Transform [] spawnPositionTransformList;
	[SerializeField] private Transform NextWaveSpawnPositionTransform;
	private State state;
	private int waveNumber;
	private float nextWaveSpawnTimer;
	private float nextEnemySpawnTimer;
	private int remainingEnemySpawnAmount;
	private Vector3 spawnPosition;

	private void Start ()
	{
		state = State.WaitingToSpawnNextWave;
		spawnPosition = spawnPositionTransformList [UnityEngine.Random.Range (0, spawnPositionTransformList.Length)].position;
		NextWaveSpawnPositionTransform.position = spawnPosition;
		nextWaveSpawnTimer = 3f;
	}

	private void Update ()
	{
		switch (state) {
		case State.WaitingToSpawnNextWave:
			nextWaveSpawnTimer -= Time.deltaTime;
			if (nextWaveSpawnTimer < 0) {
				SpawnWave ();
			}
			break;
		case State.SpawningWave:
			if (remainingEnemySpawnAmount > 0) {
				nextEnemySpawnTimer -= Time.deltaTime;
				if (nextEnemySpawnTimer < 0) {
					nextEnemySpawnTimer = UnityEngine.Random.Range (0f, enemySpawnDelay);
					Enemy.Create (spawnPosition + UtilsClass.GetRandomDir () * UnityEngine.Random.Range (0, 10f));
					remainingEnemySpawnAmount--;

					if (remainingEnemySpawnAmount <= 0) {
						state = State.WaitingToSpawnNextWave;
						spawnPosition = spawnPositionTransformList [UnityEngine.Random.Range (0, spawnPositionTransformList.Length)].position;
						NextWaveSpawnPositionTransform.position = spawnPosition;
					}
				}
			}

			break;
		}
	}

	private void SpawnWave ()
	{
		//Time to spawn next wave
		nextWaveSpawnTimer = 10f;
		//Increase wave difficulty
		remainingEnemySpawnAmount = enemySpawnAmount + enemyIncreasePerWave * waveNumber;
		state = State.SpawningWave;
		waveNumber++;
		OnWaveNumberChanged?.Invoke (this, EventArgs.Empty);
	}

	public int GetWaveNumber ()
	{
		return waveNumber;
	}

	public float GetNextWaveSpawnTimer ()
	{
		return nextWaveSpawnTimer;
	}

	public Vector3 GetSpawnPosition ()
	{
		return spawnPosition;
	}
}
