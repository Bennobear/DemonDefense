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
    [Header("Versusmode")]
    public int cost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    
}
