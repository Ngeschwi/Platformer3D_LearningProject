using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public Transform _target;
    public Vector3 _offset;

    void Start() {
        
        _offset = _target.position - transform.position;
    }

    void Update() {
        transform.position = _target.position - _offset;
    }
}
