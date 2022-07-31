using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _player;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _health = 2;

    private bool _attack;
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_player.position, transform.position);
        if(distance > _attackDistance)
        {
            _agent.SetDestination(_player.position);
            _attack = false;
        }
        else
        {
            _agent.SetDestination(transform.position);
            _attack = true;
        }
        _anim.SetBool("Attack", _attack);

        if (_health < 1)
        {
            _anim.SetTrigger("Death");
            enabled = false;
        }
    }
}
