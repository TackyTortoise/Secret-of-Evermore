using UnityEngine;
using UnityEngine.AI;

public class Script_EnemyBehaviour : Script_VisualCharacter
{
    private float _baseAttackRange = 2;
    private float _followPlayerRange = 10f;

    private float _viewAngle = 90f;

    private const float _attackInterval = .7f;
    private float _attackTimer = 0f;

    private NavMeshAgent _navAgent;


    void Start()
    {
        _attackTimer = _attackInterval;
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.Warp(transform.position);
    }


    void Update()
    {
        //Follow selected character when in followrange
        var selectedChar = Script_GameManager.GetInstance().CharacterManager.GetSelectedCharacter().GetVisualCharacter();
        if ((selectedChar.transform.position - transform.position).sqrMagnitude <= _followPlayerRange * _followPlayerRange)
            _navAgent.destination = selectedChar.transform.position;
        //Stand still if not in range
        else
            _navAgent.destination = transform.position;

        _attackTimer += Time.deltaTime;

        //Attack
        if (_attackTimer >= _attackInterval)
        {
            //Get characters in cone of vision
            foreach (var c in Script_GameManager.GetInstance().CharacterManager.GetCharacters())
            {
                var between = c.GetVisualCharacter().transform.position - transform.position;
                var angle = Vector3.Angle(transform.forward, between);
                if (angle <= _viewAngle / 2f && between.sqrMagnitude <= _baseAttackRange * _baseAttackRange)
                {
                    //Hit player
                    c.TakeDamage(_attachedCharacter.Attack);

                    _attackTimer = 0f;
                    break; //Only can damage one character each attack
                }
            }
        }
    }
}
