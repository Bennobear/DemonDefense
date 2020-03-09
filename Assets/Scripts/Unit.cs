using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Basics")]
    public float movementSpeed;
    public int life;
    public int damage;
    public int reward;
    [Header("Utility")]
    public float holy;
    public bool isFlying;
    public bool isInvisible;
    //miscellaneous effects
    [Header("Hackermode")]
    public int cost;
    public HealthbarEffect healthUnit;

    //Movement
    private Transform target;
    private int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * movementSpeed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            //Reached BASE DO SOMETHING
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    public void getDamage(int _damage)
    {
        life -= _damage;
        healthUnit.hitDamage(_damage);
    }
}
