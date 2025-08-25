using UnityEngine;
using UnityEngine.AI;

public class HemAI : MonoBehaviour
{
    public Transform target;
    public float catchDistance = 1.8f;
    public System.Action OnCatch;
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!target) return;
        if (agent) agent.SetDestination(target.position);
        float d = Vector3.Distance(transform.position, target.position);
        if (d <= catchDistance)
        {
            OnCatch?.Invoke();
        }
    }
}
