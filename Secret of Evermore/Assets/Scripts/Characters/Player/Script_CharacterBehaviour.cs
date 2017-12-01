using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Script_CharacterBehaviour : Script_VisualCharacter
{
    private CharacterController _controller;
    [SerializeField]
    private float _moveSpeed = 10f;
    private const float _totalChargeTime = 1.5f;
    private float _currentChargeTimer = 0f;
    private bool _currentlyActive = true;
    private NavMeshAgent _navAgent;
    private Transform _followTarget;

    void Start()
    {
        _controller = gameObject.AddComponent<CharacterController>();
        _currentChargeTimer = _totalChargeTime;
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.speed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentlyActive)
        {
            Movement();
            Attack();
        }
        else
            FollowTarget();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Attack with weapon if you have one
            if (_attachedCharacter.Weapon != null)
            {
                var hit = _attachedCharacter.Weapon.GetHitEnemies(this);
                foreach (var e in hit)
                {
                    e.TakeDamage(GetAttackDamage());
                }
            }
            else
            {
                RaycastHit hit;
                Debug.DrawLine(transform.position + transform.forward, transform.position + 2f * transform.forward, Color.red, 3f);
                if (Physics.Raycast(transform.position + transform.forward, transform.forward, out hit, 2f))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.GetComponent<Script_EnemyBehaviour>().GetAttachedCharacter().TakeDamage(GetAttackDamage());
                    }
                }
            }
            _currentChargeTimer = 0f;
        }

        if (_currentChargeTimer < _totalChargeTime)
        {
            _currentChargeTimer += Time.deltaTime;
            if (_currentChargeTimer > _totalChargeTime)
                _currentChargeTimer = _totalChargeTime;
        }
    }

    void Movement()
    {
        float inputHor = Input.GetAxis("Horizontal");
        float inputVer = Input.GetAxis("Vertical");

        //Only four directional movement
        if (inputHor > 0f)
            inputVer = 0f;

        var move = inputHor * Vector3.right + inputVer * Vector3.forward;

        //Rotate to move direction
        if (move.sqrMagnitude > .1f * .1f)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move),
                10f * Time.deltaTime);

        _controller.Move(_moveSpeed * move * Time.deltaTime);
    }

    public void FollowTarget()
    {
        _navAgent.isStopped = false;
        _navAgent.destination = _followTarget.position;
    }

    public float GetCurrentChargePercentage()
    {
        return _currentChargeTimer / _totalChargeTime;
    }

    public int GetAttackDamage()
    {
        if (_attachedCharacter.Weapon == null)
            return _attachedCharacter.Attack;
        return (int)(_attachedCharacter.Weapon.GetPower() * GetCurrentChargePercentage() + _attachedCharacter.Attack);
    }

    public void SetActiveCharacter(bool active)
    {
        _currentlyActive = active;
        if (_navAgent == null)
            _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.isStopped = true;
    }

    public void SetFollowTarget(Transform target)
    {
        _followTarget = target;
    }
}
