using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CharacterBehaviour : Script_VisualCharacter
{
    private CharacterController _controller;
    private float _moveSpeed = 10f;
    private const float _totalChargeTime = 1.5f;
    private float _currentChargeTimer = 0f;

    void Start()
    {
        _controller = gameObject.AddComponent<CharacterController>();
        _currentChargeTimer = _totalChargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Movement();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attacking");
            var hit = _attachedCharacter.Weapon.GetHitEnemies(this);
            foreach (var e in hit)
            {
                e.TakeDamage(GetAttackDamage());
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

    public float GetCurrentChargePercentage()
    {
        return _currentChargeTimer / _totalChargeTime;
    }

    public int GetAttackDamage()
    {
        return (int) (_attachedCharacter.Weapon.GetPower() * GetCurrentChargePercentage() + _attachedCharacter.Attack);
    }
}
