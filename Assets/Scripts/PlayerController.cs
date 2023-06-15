using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    public float _moveSpeed;
    public float _jumpForce;
    public float _gravity;
    public Vector3 _moveDirection;
    private Animator _anim;
    private bool _isWalking;
    public bool _canMove = true;
    private float _horizontalInput;
    private float _verticalInput;
    public Camera _Camera;
    
    void Start() {
        
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }
    
    void Update() {
        
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        Vector3 cameraForward = _Camera.transform.forward;
        Vector3 moveDirection = cameraForward * _verticalInput + _Camera.transform.right * _horizontalInput;
        
        _moveDirection = new Vector3(
                    moveDirection.x * _moveSpeed,
                    _moveDirection.y,
                    moveDirection.z * _moveSpeed);
        
        if (Input.GetKeyDown(KeyCode.Space) && _controller.isGrounded) {
            _moveDirection.y = _jumpForce;
        }

        if (!_controller.isGrounded) {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }
        
        if (_moveDirection.x != 0 || _moveDirection.z != 0) {
            _isWalking = true;
            
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(new Vector3(_moveDirection.x, 0, _moveDirection.z)), 
                0.15f);
        } else {
            _isWalking = false;
        }
        
        _anim.SetBool("IsWalking", _isWalking);
    }

    private void FixedUpdate() {
        
        if (_canMove) {
            _controller.Move(_moveDirection * Time.deltaTime);
        }
    }
}
