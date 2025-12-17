using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public Transform[] waypoints;
    Rigidbody rb;
    int currentWaypointIndex;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void FixedUpdate()
    {
        Transform currentWaypoint = waypoints[currentWaypointIndex];
        Vector3 currentToTarget = currentWaypoint.position - rb.position;
        if (currentToTarget.magnitude < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        Quaternion forwardRotation = Quaternion.LookRotation(currentToTarget);
        rb.MoveRotation(forwardRotation);
        rb.MovePosition(rb.position + 
            currentToTarget.normalized * moveSpeed * Time.deltaTime);
    }
}
