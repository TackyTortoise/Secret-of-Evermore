using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CharacterBehaviour : MonoBehaviour
{
    private Camera _mainCamera;
    private CharacterController _controller;
    private float _moveSpeed = 10f;
    private Script_Character _attachedCharacter;

    void Start()
    {
        _mainCamera = Camera.main;
        _controller = gameObject.AddComponent<CharacterController>();
    }

    public void SetCharacter(Script_Character character)
    {
        _attachedCharacter = character;
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
            _attachedCharacter.GetWeapon().Attack(gameObject);
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
}
