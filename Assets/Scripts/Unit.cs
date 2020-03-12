using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
//Class containing all variables and methods of a enemy

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
    public Animator lifeAnimator;

    //Movement
    private Transform target;
    private int waypointIndex = 0;
    private SpriteRenderer mySpriteRenderer;

    //Gets its own SpriteRenderer component to flip the sprite when walking -x
    private void Awake()
    {
        lifeAnimator = GameObject.Find("Life").GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    //Get the array of waypoints to create a path to walk
    void Start()
    {
        target = Waypoints.waypoints[0];
    }
    // Walk from waypoint to waypoint and flip the sprite when walking -x
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
            PlayerStats.money += reward;
            Destroy(gameObject);
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
    //After reaching a waypoint getting the next one to target it as direction 
    //Destroying itself after reaching the last waypoint and hit the player
    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            PlayerStats.life -= damage;
            //Reached BASE DO SOMETHING
            Destroy(gameObject);
            lifeAnimator.SetTrigger("itHappened");
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }
    //Taking damage from a tower bullet
    public void getDamage(int _damage)
    {
        life -= _damage;
        healthUnit.hitDamage(_damage);
    }
}
