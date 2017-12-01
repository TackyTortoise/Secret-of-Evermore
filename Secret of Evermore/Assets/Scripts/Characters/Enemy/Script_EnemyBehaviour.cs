using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Script_EnemyBehaviour : Script_VisualCharacter
{
    private float _baseAttackRange = 2;
    private float _followPlayerRange = 10f;

    private float _viewAngle = 90f;

    private const float _attackInterval = .4f;
    private float _attackTimer = 0f;

    private NavMeshAgent _navAgent;
    // Use this for initialization
    void Start()
    {
        _attackTimer = _attackInterval;
        _navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var selectedChar = Script_GameManager.GetInstance().CharacterManager.GetSelectedCharacter().GetVisualCharacter();
        if ((selectedChar.transform.position - transform.position).sqrMagnitude <=
            _followPlayerRange * _followPlayerRange)
            _navAgent.destination = selectedChar.transform.position;
        else
            _navAgent.destination = transform.position;

        _attackTimer += Time.deltaTime;

        if (_attackTimer >= _attackInterval)
        {
            foreach (var c in Script_GameManager.GetInstance().CharacterManager.GetCharacters())
            {
                var between = c.GetVisualCharacter().transform.position - transform.position;
                var angle = Vector3.Angle(transform.forward, between);
                if (angle <= _viewAngle / 2f && between.sqrMagnitude <= _baseAttackRange * _baseAttackRange)
                {
                    c.TakeDamage(_attachedCharacter.Attack);

                    _attackTimer = 0f;
                    break; //Only can damage one character each attack
                }
            }
        }
    }
}
