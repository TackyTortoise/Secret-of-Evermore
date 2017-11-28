using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CharacterBehaviour : MonoBehaviour
{
    private Camera _mainCamera;
    private CharacterController _controller;
    private float _moveSpeed = 10f;

    void Start()
    {
        _mainCamera = Camera.main;
        _controller = gameObject.AddComponent<CharacterController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float inputHor = Input.GetAxis("Horizontal");
        float inputVer = Input.GetAxis("Vertical");

        //Only four directional movement
        if (inputHor > 0f)
            inputVer = 0f;

        var move = inputHor * Vector3.right + inputVer * Vector3.forward;

        //Rotate to move direction
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move),
            10f * Time.deltaTime);

        _controller.Move(_moveSpeed * move * Time.deltaTime);
    }
}
