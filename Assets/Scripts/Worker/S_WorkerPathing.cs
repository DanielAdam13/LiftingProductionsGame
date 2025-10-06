using UnityEngine;
using UnityEngine.AI;

public class WorkerPathing : MonoBehaviour
{
    public Transform[] points;           // Array of waypoints
    public float waitTime = 2f;          // Wait time at each point
    private int destPoint = 0;
    private NavMeshAgent agent;
    private float waitTimer = 0f;
    private bool waiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (points.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned to " + gameObject.name);
            enabled = false;
            return;
        }

        GoToNextPoint();
    }

    void Update()
    {
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waiting = false;
                GoToNextPoint();
            }
            return;
        }

        // Check if agent reached the destination
        if (!agent.pathPending && agent.remainingDistance < 0.3f)
        {
            waiting = true;
            waitTimer = 0f;
        }
    }

    void GoToNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length; // Loop around
    }
}
