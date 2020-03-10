using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	public Text lifeText;
	public Text moneyText;

	public static int money;
	public int startMoney = 400;

	public static int life;
	public int startLives = 20;

	public static int Rounds;

	void Start()
	{
		money = startMoney;
		life = startLives;

		Rounds = 0;
	}

	private void Update()
	{
		lifeText.text = Mathf.Round(life).ToString();
		moneyText.text = Mathf.Round(money).ToString();
	}
	public void TakeDamage(int amount)
	{
		life -= amount;
	}

	public void Heal(int amount)
	{
		life += amount;
	}

	public void KillReward(int amount)
	{
		money += amount;
	}

	public void BuyTower(int amount)
	{
		money -= amount;
	}
}