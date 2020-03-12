using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//This Class handles our waves of enemies 

public class WaveSpawner : MonoBehaviour
{
	public Transform[] enemyList;
	public Transform randomEnemy;
	public Text waveText;

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

	//This Start Method starts two of our Methods immediately but they are updated every .5 seconds (To prevent two waves spawning at the same time) 
	private void Start()
	{
		InvokeRepeating("UpdateEnemyList", 0f, 0.5f);
		InvokeRepeating("UpdateCountdown", 0f, 0.5f);
	}
	//Puts every enemy on the scene in a array
	void UpdateEnemyList()
	{
		enemies = GameObject.FindGameObjectsWithTag(enemyTag);
	}
	//As long we're not in the last wave, when the enemies array is empty activate the timer again
	public void UpdateCountdown() {
		if (waveIndex != maxWaves)
		{
			if (enemies.Length <= 0)
			{
				waveCountdownText.gameObject.SetActive(true);
			}
		}
	}
	//Called when we're in the last wave
	public void CheckForWin()
	{
		if (enemies.Length == 0 && isDone == true)
		{
			Debug.Log("Gewonnen");
			GameManager gm = gameObject.GetComponent<GameManager>();
			gm.WinLevel();
		}
	}
	//Handles different states of the game
	void Update()
	{
		waveText.text = "Wave " + waveIndex + " / " + maxWaves;
		//When the game is over (Won/Lose) stop enemy and time check
		if (GameManager.gameIsOver == true)
		{
			isDone = false;
			CancelInvoke("UpdateCountdown");
			CancelInvoke("CheckForWin");
		}
		//When countdown reaches 0 spawn next wave and deactivate the Countdown Object
		if (countdown <= 0f)
		{
			countdown = timeBetweenWaves;
			waveCountdownText.gameObject.SetActive(false);
			StartCoroutine(SpawnWave());
		}
		//As long as the timer is active = count down
		if (waveCountdownText.gameObject.activeSelf)
		{
			countdown -= Time.deltaTime;
			waveCountdownText.text = Mathf.Round(countdown).ToString();
		}
	}
	//Coroutine to spawn a wave with random enemies each time
	//Stop updating the countdown as long as the routine runs to prevent two waves spawning at once
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
		//If we reach the last wave we activate a method that runs every .5 seconds
		if (waveIndex >= maxWaves && isDone == false)
		{
			InvokeRepeating("CheckForWin", 0f, 0.5f);
			isDone = true;
			yield break;
		}
		InvokeRepeating("UpdateCountdown", 1f, 1f);
		amountEnemies += 5;
	}
	//Simple method to spawn the enemy 
	void SpawnEnemy(Transform enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}
	//Method to wait for given amount of time
	IEnumerator Wait(float time)
	{
		yield return new WaitForSeconds(time);
	}
}