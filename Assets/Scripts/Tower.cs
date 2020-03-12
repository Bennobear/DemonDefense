using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//Class containing all variables and methods of a tower

public class Tower : MonoBehaviour
{
    [Header("Basics")]
    public int cost;
    public int damage;
    public float range;
    public float attackSpeed;
    private float fireCountdown = 0f;
    public float horror;
    public float turnSpeed = 0;
    [Header("Utility")]
    public Transform target;
    private Unit targetObj;
    private string enemyTag = "Enemy";
    public bool canAttackFlying;
    public bool canAttackInvisible;
    //miscellaneous effects like Splash Attack / Slow 
    [Header("Upgrades")]
    public List<Upgrades> upgrades;
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    TestOurTile buildManager;

    //Update target every .5 seconds and get our buildManager Singleton Instance 
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        buildManager = TestOurTile.instance;
    }
    //Detect nearest Unit and Target it
    void UpdateTarget()
    {
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

    //No target = repeat 
    //Roate the turret towards the target (not necessary as for now so speed is 0) 
    //Fire a the target with a set countdown 
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

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Shop.Hide_Static();
            }
        }
    }
    //Create a bullet and give it all the information it needs to find and hit the target
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if (bullet != null)
            bullet.Seek(target, damage);
    }
    //DEBUG PURPOSE
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    //Upgrade the range of a tower
    public void UpgradeRange()
    {
        range += 0.5f;
    }
    //Upgrade the damage of a tower
    public void UpgradeDamage()
    {
        damage += 5;
    }
    //Sell a tower 
    public void SellTower()
    {
        Destroy(gameObject);
        buildManager.DeleteTower(this);
        UpgradeOverlay.Hide_Static();
    }
    //Open the Overlay when clicking on a tower
    private void OnMouseDown()
    {
        UpgradeOverlay.Show_Static(this);
        Debug.Log("Show Static Upgrade Overlay");
    }
    //Return the range of a tower
    public float GetRange()
    {
        return range;
    }
    //Return the cost of a tower
    public int GetPrice()
    {
        return cost;
    }
}