using System;
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
    private bool _sprinting = false;

    void Start()
    {
        _controller = gameObject.AddComponent<CharacterController>();
        _currentChargeTimer = _totalChargeTime;
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.speed = _moveSpeed;
        _navAgent.Warp(transform.position);
        _navAgent.destination = transform.position;
        _navAgent.ResetPath();
    }


    void Update()
    {
        //Being controlled by player
        if (_currentlyActive)
        {
            //Move character
            Movement();

            //Check charge percentage
            if (_currentChargeTimer < _totalChargeTime)
            {
                _currentChargeTimer += Time.deltaTime;
                if (_currentChargeTimer > _totalChargeTime)
                {
                    _currentChargeTimer = _totalChargeTime;
                    _sprinting = false;
                }
            }

            //Check for attack
            Attack();
        }
        //Follow other character automatically
        else
            FollowTarget();
    }


    void Attack()
    {
        //Left mouse button pressed
        if (Input.GetMouseButtonDown(0) && Script_GameManager.GetInstance().UIManager.ActivePanels == 0)
        {
            //Attack with weapon if you have one
            if (_attachedCharacter.Weapon != null)
            {
                //Check what was hit by weapon
                var hit = _attachedCharacter.Weapon.GetHitObjects(this);
                foreach (var o in hit)
                {
                    //Deal damage to hit enemies
                    if (o.tag == "Enemy")
                    {
                        var es = o.GetComponent<Script_EnemyBehaviour>();
                        if (es != null)
                        {
                            es.GetAttachedCharacter().TakeDamage(GetAttackDamage());
                        }
                    }

                    //Destruct destructables
                    if (o.tag == "Destructable")
                    {
                        var ds = o.GetComponent<Script_Destructable>();
                        if (ds != null)
                        {
                            ds.Destruct(_attachedCharacter.Weapon);
                        }
                    }
                }
            }
            //Hit enemy right in front with "fist"
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, ~(1 << LayerMask.NameToLayer("Player"))))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.GetComponent<Script_EnemyBehaviour>().GetAttachedCharacter().TakeDamage(GetAttackDamage());
                    }
                }
            }
            _currentChargeTimer = 0f;
        }


    }

    void Movement()
    {
        float inputHor = Input.GetAxis("Horizontal");
        float inputVer = Input.GetAxis("Vertical");

        //Only four directional movement
        if (Math.Abs(inputHor) > .2f)
            inputVer = 0f;

        var move = inputHor * Vector3.right + inputVer * Vector3.forward;

        //Rotate to move direction
        if (move.sqrMagnitude > .1f * .1f)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move),
                10f * Time.deltaTime);

        //Check sprint button press
        if (Input.GetKeyDown(KeyCode.LeftShift) && Math.Abs(_currentChargeTimer - _totalChargeTime) < 1e-5)
        {
            _sprinting = true;
            _currentChargeTimer = 0;
        }

        var speed = _sprinting ? _moveSpeed * 1.5f : _moveSpeed;
        _controller.Move(speed * move * Time.deltaTime);
    }

    public void FollowTarget()
    {
        //_navAgent.isStopped = false;
        _navAgent.destination = _followTarget.position;
        if ((_followTarget.position - transform.position).sqrMagnitude <=
            _navAgent.stoppingDistance * _navAgent.stoppingDistance)
            _navAgent.destination = transform.position;
    }

    public float GetCurrentChargePercentage()
    {
        return _currentChargeTimer / _totalChargeTime;
    }

    public int GetAttackDamage()
    {
        if (_attachedCharacter.CharType == Script_Character.CharacterType.Dog)
        {
            return _attachedCharacter.Attack;
        }

        int equipedBoost = 0;
        Script_GameManager.GetInstance().Inventory.GetEquipedItems().ForEach(x => equipedBoost+=x.AttackBoost);
        return (int)(equipedBoost * GetCurrentChargePercentage()) + _attachedCharacter.Attack;
    }

    public void SetActiveCharacter(bool active)
    {
        _currentlyActive = active;
        if (_navAgent == null)
            _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.isStopped = active;
    }

    public void SetFollowTarget(Transform target)
    {
        _followTarget = target;
    }
}
