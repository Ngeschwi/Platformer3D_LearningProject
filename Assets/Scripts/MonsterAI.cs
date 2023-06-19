using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [Range(0.5f, 50f)]
    public float _detectRange = 3f;
    public Transform[] _points;
    public int _NbrOfPoints = 3;
    public NavMeshAgent _agent;
    private int _destinationsIndex = 0;
    private Transform _player;
    private float _MonsterSpeed = 1f;
    private float _MonsterSpeedChase = 2f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_points[_destinationsIndex].position);
    }

    private void Update() {
        
        Walk();
        Chase();
    }

    public void Chase() {
        
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (distanceToPlayer <= _detectRange) {
            _agent.SetDestination(_player.position);
            _agent.speed = _MonsterSpeedChase;
        } else {
            _agent.SetDestination(_points[_destinationsIndex].position);
            _agent.speed = _MonsterSpeed;
        }
    }
    
    public void Walk() {
        
        float distance = _agent.remainingDistance;
        if (distance <= 0.05f) {
            _destinationsIndex++;
            if (_destinationsIndex >= _NbrOfPoints) {
                _destinationsIndex = 0;
            }
            _agent.SetDestination(_points[_destinationsIndex].position);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
}
