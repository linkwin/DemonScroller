using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public List<Transform> waypoints = new List<Transform>();

	
	void Update () {

        Transform[] tem = GetComponentsInChildren<Transform> ();

        if (tem.Length > 0)
        {
            waypoints.Clear();

            foreach (Transform t in tem)
                waypoints.Add(t);

        }
	}

    void OnDrawGizmos ()
    {

        if (waypoints.Count > 0)
        {

            Gizmos.color = Color.green;


            foreach (Transform t in waypoints)
                Gizmos.DrawSphere(t.position, 1f);

            Gizmos.color = Color.red;
        }
        for (int a = 0; a < waypoints.Count - 1; a++)
        { 
            Gizmos.DrawLine (waypoints[a].position, waypoints[a + 1].position);
        }
    }
}
