using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]private NavMeshAgent _agent;
    [SerializeField]private Transform _EndPoint;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _EndPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
