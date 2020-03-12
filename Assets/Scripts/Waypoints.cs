using UnityEngine;
//This class creates a static array containing our waypoints 

public class Waypoints : MonoBehaviour
{
    public static Transform[] waypoints;
    void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
