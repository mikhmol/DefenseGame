using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // waypoints array
    public static Transform[] waypoints;

    private void Awake()
    {
        // count all child gameobjects from Waypoits and use it as a Length of array
        waypoints = new Transform[transform.childCount];

        // assign each waypoint to an array
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
