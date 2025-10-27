using UnityEngine;
using UnityEngine.AI;

public class MazeWalker : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _agent.SetDestination(_target.position);
    }


    private NavMeshAgent _agent;
}
