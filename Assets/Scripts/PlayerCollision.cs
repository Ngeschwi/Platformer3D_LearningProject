using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int _nbrCoins = 0;
    public GameObject _PickUpEffect;
    public GameObject _KillMobEffect;
    public GameObject _Camera;
    private Vector3 _HitDirection;
    private Vector3 _JumpDirection;
    private float _rotationCamera1 = -90f;
    private float _rotationCamera2 = 180f;

    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag == "Coin") {
            GameObject go = Instantiate(_PickUpEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(go, 0.5f);
            _nbrCoins++;
        }

        if (other.gameObject.tag == "Cam1") {
            _Camera.transform.RotateAround(transform.position, Vector3.up, _rotationCamera1);
            //_Camera.transform.LookAt(transform.position);
        } else if (other.gameObject.tag == "Cam2") {
            _Camera.transform.RotateAround(transform.position, Vector3.up, _rotationCamera2);
        }
    }

    private void OnTriggerExit(Collider other) {
        
        if (other.gameObject.tag == "Cam1") {
            _Camera.transform.RotateAround(transform.position, Vector3.up, -1 * _rotationCamera1);
        } else if (other.gameObject.tag == "Cam2") {
            _Camera.transform.Rotate(0f, -1 * _rotationCamera2, 0f, Space.Self);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other) {
    
        if (other.gameObject.tag == "Hurt") {
            print("Aie !");
            StepBackWhenHit();
        } else if (other.gameObject.tag == "Enemy") {
            print("Tue !");
            GameObject go = Instantiate(_KillMobEffect, other.transform.position, Quaternion.identity);
            JumpOnEnemy();
            Destroy(other.gameObject.transform.parent.gameObject);
            Destroy(go, 0.5f);
        }
    }
    
    private void StepBackWhenHit() {
        
        GetComponent<PlayerController>()._canMove = false;
        
        _HitDirection = transform.position - transform.forward * 2;
        iTween.MoveTo(gameObject, iTween.Hash(
            "position", _HitDirection,
            "time", 0.5f,
            "easetype", iTween.EaseType.easeOutBack,
            "oncomplete", "EnableMovement"));
    }

    private void JumpOnEnemy() {
        
        GetComponent<PlayerController>()._canMove = false;
        
        _JumpDirection = transform.position + transform.up * 2;
        iTween.MoveTo(gameObject, iTween.Hash(
            "position", _JumpDirection,
            "time", 0.5f,
            "easetype", iTween.EaseType.easeOutBack,
            "oncomplete", "EnableMovement"));
    }
    
    private void EnableMovement() {
        
        GetComponent<PlayerController>()._canMove = true;
    }
}
