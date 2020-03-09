using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Basics")]
    public int cost;
    public int damage;
    public float range;
    public float attackSpeed;
    private float fireCountdown = 0f;
    public float horror;
    public float turnSpeed = 10;
    [Header("Utility")]
    public Transform target;
    public string enemyTag = "Enemy";
    public bool canAttackFlying;
    public bool canAttackInvisible;
    //miscellaneous effects like Splash Attack / Slow 
    [Header("Upgrades")]
    public List<Upgrades> upgrades;
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;



    TestOurTile buildManager;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        buildManager = TestOurTile.instance;
    }

    // Update is called once per frame
    void UpdateTarget()
    {
        //Detect nearest Unit and Target it
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        //Rotate Turret to nearest Target
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 actualRotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, 0f, actualRotation.z);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / attackSpeed;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target, damage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    public void UpgradeRange()
    {
        range += 0.5f;
    }

    public void UpgradeDamage()
    {
        damage += 5;
    }

    public void SellTower()
    {
        Destroy(gameObject);
        buildManager.DeleteTower(this);
        UpgradeOverlay.Hide_Static();
    }

    private void OnMouseDown()
    {
        UpgradeOverlay.Show_Static(this);
    }

    public float GetRange()
    {
        return range;
    }

    public int GetPrice()
    {
        return cost;
    }

}