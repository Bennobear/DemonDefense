using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{

	public static int Money;
	public int startMoney = 400;

	public static int Lives;
	public int startLives = 20;

	public static int Rounds;

	void Start()
	{
		Money = startMoney;
		Lives = startLives;

		Rounds = 0;
	}
	public void TakeDamage(int amount)
	{
		Lives -= amount;
	}

	public void Heal(int amount)
	{
		Lives += amount;
	}

	public void KillReward(int amount)
	{
		Money += amount;
	}

	public void BuyTower(int amount)
	{
		Money -= amount;
	}
}