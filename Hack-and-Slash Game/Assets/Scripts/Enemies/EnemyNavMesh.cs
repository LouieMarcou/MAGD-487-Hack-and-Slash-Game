using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField] private Transform playerPositionTransform;
    public NavMeshAgent navMeshAgent;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.destination = playerPositionTransform.position;
    }

    public void SetPlayerPositionTransform(Transform transform)
    {
        playerPositionTransform = transform;
    }
}
