using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _InstancePlayerController;
    
    private CharacterController _controller;
    private float _moveSpeed;
    public float _walkSpeed;
    public float _runSpeed;
    public float _jumpForce;
    public float _gravity;
    public Vector3 _moveDirection;
    private Animator _anim;
    private bool _isWalking;
    public bool _canMove = true;
    private float _horizontalInput;
    private float _verticalInput;
    public Camera _Camera;

    private void Awake() {
        
        _InstancePlayerController = this;
    }

    void Start() {
        
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }
    
    void Update() {
        
        Vector3 moveDirection = _Camera.transform.forward * Input.GetAxis("Vertical")
                                + _Camera.transform.right * Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftControl)) {
            _moveSpeed = _runSpeed;
        } else {
            _moveSpeed = _walkSpeed;
        }
        
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
