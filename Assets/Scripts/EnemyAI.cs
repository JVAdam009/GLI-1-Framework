using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        SpawningManager.Instance.RequestNewDestination(_agent);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
