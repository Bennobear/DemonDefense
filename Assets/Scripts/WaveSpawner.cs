using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public Transform enemyOne;
	public Transform enemyTwo;
	public Transform enemyThree;
	public Transform randomEnemy;

	public Transform spawnPoint;

	public int maxWaves;
	public int amountEnemies;
	public float timeBetweenWaves = 5f;
	private float countdown = 2f;
	public GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
	public Text waveCountdownText;

	private int waveIndex = 0;

	void Update()
	{
		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}

		countdown -= Time.deltaTime;

		waveCountdownText.text = Mathf.Round(countdown).ToString();

		if (waveIndex >= maxWaves)
		{
			if(enemies[0] = null)
			{
				GameManager gm = gameObject.GetComponent<GameManager>();
				gm.WinLevel();
			}
		}

	}

	IEnumerator SpawnWave()
	{
		waveIndex++;
		PlayerStats.Rounds++;
		for (int i = 0; i < amountEnemies; i++)
		{
			SpawnEnemy(randomEnemy);
			yield return new WaitForSeconds(0.5f);
		}
		amountEnemies += 10;
	}

	void SpawnEnemy(Transform enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}

	

}