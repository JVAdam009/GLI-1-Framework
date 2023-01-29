using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    enum States
    {
        Running,
        Hiding,
        Death
    }
    [SerializeField]private NavMeshAgent _agent;
    [SerializeField]private Animator _animator;

    [SerializeField] private int _currentSpot = 0;
    [SerializeField] private int _increment = 4;

    [SerializeField] private States _state = States.Running;
    [SerializeField] private AudioClip _deathSFX;

    private bool _startHiding = false;
    private bool _startDying = false;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = GameManager.Instance.GenerateNextHidingSpot(ref _currentSpot,_increment);

    }

    private void OnEnable()
    {
        _currentSpot = 0;
        _agent.destination = GameManager.Instance.GenerateNextHidingSpot(ref _currentSpot,_increment);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case States.Running:
                RunState();
                break;
            case States.Hiding:
                HideState();
                break;
            case States.Death:
                DeathState();
                break;
        }
    }

    private void DeathState()
    {
        if (_startDying)
        {
            _startDying = false;
            _animator.SetTrigger("Death");
            UIManager.Instance.EnemyDestroyed();
            GameManager.Instance.EnemyDestoryed();
            _agent.isStopped = true;
            AudioSource.PlayClipAtPoint(_deathSFX,transform.position);
        }
    }

    private void HideState()
    {
        if (_startHiding)
        {
            _startHiding = false;
            StartCoroutine(HideFromPlayer());
        }
    }

    IEnumerator HideFromPlayer()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        _agent.destination = GameManager.Instance.GenerateNextHidingSpot(ref _currentSpot,_increment);
        _agent.isStopped = false;
        _state = States.Running;
    }
    private void RunState()
    {
        _animator.SetFloat("Speed",_agent.speed);
        if (_agent.remainingDistance <= 0.1f)
        {
            _agent.isStopped = true;
            _state = States.Hiding;
            _startHiding = true;
        }
    }

    public void GotHit()
    {
        Debug.Log("I Got hit");
        _state = States.Death;
        _startDying = true;
    }
}
