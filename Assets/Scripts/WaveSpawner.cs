using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public Transform[] enemyList;
	public Transform randomEnemy;

	public Transform spawnPoint;
	private string enemyTag = "Enemy";
	public GameObject[] enemies;
	public int maxWaves;
	public int amountEnemies;
	public float timeBetweenWaves = 5.5f;
	private float countdown = 10.5f;
	public Text waveCountdownText;
	public bool isDone = false;

	private int waveIndex = 0;

	private void Start()
	{
		InvokeRepeating("UpdateEnemyList", 0f, 0.5f);
		InvokeRepeating("UpdateCountdown", 0f, 0.5f);
	}

	void UpdateEnemyList()
	{
		enemies = GameObject.FindGameObjectsWithTag(enemyTag);
	}

	public void UpdateCountdown() {
		if (waveIndex != maxWaves)
		{
			if (enemies.Length <= 0)
			{
				waveCountdownText.gameObject.SetActive(true);
			}
		}
	}

	public void CheckForWin()
	{
		if (enemies.Length == 0 && isDone == true)
		{
			Debug.Log("Gewonnen");
			GameManager gm = gameObject.GetComponent<GameManager>();
			gm.WinLevel();
		}
	}
	void Update()
	{
		if (GameManager.gameIsOver == true)
		{
			isDone = false;
			CancelInvoke("UpdateCountdown");
			CancelInvoke("CheckForWin");
		}

		if (countdown <= 0f)
		{
			countdown = timeBetweenWaves;
			waveCountdownText.gameObject.SetActive(false);
			StartCoroutine(SpawnWave());
		}

		if (waveCountdownText.gameObject.activeSelf)
		{
			countdown -= Time.deltaTime;
			waveCountdownText.text = Mathf.Round(countdown).ToString();
		}
		
		if (waveIndex >= maxWaves && isDone == false)
		{
			InvokeRepeating("CheckForWin", 0f, 0.5f);
			isDone = true;
		}
	}

	IEnumerator SpawnWave()
	{
		CancelInvoke("UpdateCountdown");
		waveIndex++;
		PlayerStats.Rounds++;
		for (int i = 0; i < amountEnemies; i++)
		{
			randomEnemy = enemyList[Random.Range(0, enemyList.Length)];
			SpawnEnemy(randomEnemy);
			yield return new WaitForSeconds(0.5f);
		}

		InvokeRepeating("UpdateCountdown", 1f, 1f);
		amountEnemies += 5;
	}

	void SpawnEnemy(Transform enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1f);
	}

}