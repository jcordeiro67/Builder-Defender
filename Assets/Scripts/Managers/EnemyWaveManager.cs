using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyWaveManager : MonoBehaviour {

	public static EnemyWaveManager Instance { get; private set; }
	public event EventHandler OnWaveNumberChanged;

	private enum State {
		WaitingToSpawnNextWave,
		SpawningWave,
	}
	[SerializeField] private float timeBetweenWaveSpawn;
	[SerializeField] private int enemySpawnAmountPerWave;
	[SerializeField] private float enemySpawnIndividualDelay;
	[SerializeField] private int enemyIncreasePerWave;
	[SerializeField] private Transform [] spawnPositionTransformList;
	[SerializeField] private Transform NextWaveSpawnPositionTransform;
	private State state;
	private int waveNumber;
	private float nextWaveSpawnTimer;
	private float nextEnemySpawnTimer;
	private int remainingEnemySpawnAmount;
	private Vector3 spawnPosition;
	private int totalEnemiesThisWave;

	private void Awake ()
	{
		Instance = this;
	}

	private void Start ()
	{
		state = State.WaitingToSpawnNextWave;
		spawnPosition = spawnPositionTransformList [UnityEngine.Random.Range (0, spawnPositionTransformList.Length)].position;
		NextWaveSpawnPositionTransform.position = spawnPosition;
		nextWaveSpawnTimer = 5f;
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
					nextEnemySpawnTimer = UnityEngine.Random.Range (0f, enemySpawnIndividualDelay);
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
		nextWaveSpawnTimer = timeBetweenWaveSpawn;
		//Increase wave difficulty
		remainingEnemySpawnAmount = enemySpawnAmountPerWave + enemyIncreasePerWave * waveNumber;
		state = State.SpawningWave;
		totalEnemiesThisWave = remainingEnemySpawnAmount;
		waveNumber++;
		OnWaveNumberChanged?.Invoke (this, EventArgs.Empty);
	}

	public int GetWaveNumber ()
	{
		return waveNumber;
	}

	public int GetTotalEnemyThisWave ()
	{
		return totalEnemiesThisWave;
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
