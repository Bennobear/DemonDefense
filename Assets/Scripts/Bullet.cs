using UnityEngine;

// This class manages the behaviour of the attack actions of "towers"
public class Bullet : MonoBehaviour
{

	private Transform target;
	private int damage;

	public float speed = 70f;
	public GameObject impactEffect;
	

	// Gets the position of the target
	public void Seek(Transform _target, int _damage)
	{
		target = _target;
		damage = _damage;
	}

	// Update is called once per frame
	void Update()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}

	// Manages the behaviour if the bullet is hitting the target
	void HitTarget()
	{
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 2f);
		Unit e = target.GetComponent<Unit>();
		bool critHit = Random.Range(0, 100) < 30;
		DamagePopUp.Create(target.localPosition, damage, critHit);
		
			if (!critHit)
			{
				e.getDamage(damage);
			}
			else
			{
				e.getDamage(damage * 2 );
			}
		
		Destroy(gameObject);

	}
}