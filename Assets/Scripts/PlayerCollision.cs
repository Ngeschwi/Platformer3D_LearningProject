using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int _nbrCoins = 0;
    private Vector3 _HitDirection;
    private Vector3 _JumpDirection;
    
    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag == "Coin") {
            Destroy(other.gameObject);
            _nbrCoins++;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other) {
    
        if (other.gameObject.tag == "Hurt") {
            print("Aie !");
            StepBackWhenHit();
        } else if (other.gameObject.tag == "Enemy") {
            print("Tue !");
            JumpOnEnemy();
            Destroy(other.gameObject.transform.parent.gameObject);
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
