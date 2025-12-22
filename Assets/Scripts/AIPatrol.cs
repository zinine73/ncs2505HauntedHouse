using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{
    public float wanderRadius = 3f;
    public float wanderTimer = 1.5f;
    NavMeshAgent agent;
    float timer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavPosition(transform.position, 
                wanderRadius, NavMesh.AllAreas);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    Vector3 RandomNavPosition(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;
        randDir += origin;
        if (NavMesh.SamplePosition(randDir, out NavMeshHit hit, dist, layerMask))
        {
            return hit.position;
        }
        else
        {
            return origin;
        }
    }
}
