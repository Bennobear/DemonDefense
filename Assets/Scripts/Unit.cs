using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Basics")]
    public float movementSpeed;
    public float life;
    public float damage;
    public int reward;
    [Header("Utility")]
    public float holy;
    public bool isFlying;
    public bool isInvisible;
    //miscellaneous effects
    [Header("Hackermode")]
    public int cost;

    //Movement
    private Transform target;
    private int waypointIndex = 0;
    private SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

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
        if (target.position.x <= transform.position.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            //Reached BASE DO SOMETHING
            PlayerStats.life -= (int)damage;
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
