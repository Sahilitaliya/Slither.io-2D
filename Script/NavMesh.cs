using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform Targate;
    NavMeshAgent Agent;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }
    private void Update()
    {
        Agent.SetDestination(Targate.position);
    }
}  