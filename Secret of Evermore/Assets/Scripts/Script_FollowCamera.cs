using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_FollowCamera : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset = new Vector3(0,10,-10);
    [SerializeField]
    private float _speed = 10f;

    public void SetTarget(Transform t)
    {
        _target = t;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _speed * Time.deltaTime);
    }
}
