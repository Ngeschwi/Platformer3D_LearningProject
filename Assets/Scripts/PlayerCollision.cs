using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject _PickUpEffect;
    public GameObject _KillMobEffect;
    public GameObject _PlayerHitEffect;
    public GameObject _Camera;
    private Vector3 _HitDirection;
    private Vector3 _JumpDirection;
    public GameObject _Cam1;
    public GameObject _Cam2;
    public AudioClip _HitSound;
    public AudioClip _CoinsSound;
    private AudioSource _audioSource;
    private bool _IsInvincible = false;
    public SkinnedMeshRenderer _renderer;
    private Color _intialColor;

    private void Start() {
        
        _audioSource = GetComponent<AudioSource>();
        _intialColor = _renderer.material.color;
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag == "Coin") {
            _audioSource.PlayOneShot(_CoinsSound);
            GameObject go = Instantiate(_PickUpEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(go, 0.5f);
            PlayerInfos._InstancePlayerInfos.GetCoin();
        }

        if (other.gameObject.tag == "Cam1") {
            _Cam1.SetActive(true);
        } else if (other.gameObject.tag == "Cam2") {
            _Cam2.SetActive(true);
        }

        if (other.gameObject.tag == "Checkpoint") {
            CheckPointManager._InstanceCheckPointManager.ActiveCheckPoint();
        }
        
        if (other.gameObject.name == "End") {
            PlayerInfos._InstancePlayerInfos.ShowFinalScore();
        }
    }

    private void OnTriggerExit(Collider other) {
        
        if (other.gameObject.tag == "Cam1") {
            _Cam1.SetActive(false);
        } else if (other.gameObject.tag == "Cam2") {
            _Cam2.SetActive(false);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other) {
    
        if (other.gameObject.tag == "Hurt" && !_IsInvincible) {
            print("Aie !");
            PlayerInfos._InstancePlayerInfos.SetHealth(-1);
            StepBackWhenHit();
        } else if (other.gameObject.tag == "Enemy") {
            print("Tue !");
            JumpOnEnemy(other.transform.parent.gameObject);
        }
        
        if (other.gameObject.tag == "Fall") {
            CheckPointManager._InstanceCheckPointManager.RespawnPlayer();
        }
    }
    
    private void StepBackWhenHit() {
        
        _IsInvincible = true;
        GetComponent<PlayerController>()._canMove = false;
        _renderer.material.color = Color.red;

        StartCoroutine(BlinkAndResetInvincible());
        
        _HitDirection = transform.position - transform.forward * 2;
        iTween.MoveTo(gameObject, iTween.Hash(
            "position", _HitDirection,
            "time", 0.5f,
            "easetype", iTween.EaseType.easeOutBack,
            "oncomplete", "EnableMovement"));

        GameObject killMobEffect = Instantiate(_PlayerHitEffect, transform.position, Quaternion.identity);
        Destroy(killMobEffect, 0.5f);
    }

    private void JumpOnEnemy(GameObject enemy) {
        
        GetComponent<PlayerController>()._canMove = false;
        
        _JumpDirection = transform.position + transform.up * 2;
        iTween.MoveTo(gameObject, iTween.Hash(
            "position", _JumpDirection,
            "time", 0.5f,
            "easetype", iTween.EaseType.easeOutBack,
            "oncomplete", "EnableMovement"));
        
        iTween.PunchScale(enemy, iTween.Hash(
            "amount", new Vector3(150f, 150f, 0.1f),
            "time", 0.5f));
        
        _audioSource.PlayOneShot(_HitSound);
        
        GameObject killMobEffect = Instantiate(_KillMobEffect, enemy.transform.position, Quaternion.identity);
        Destroy(killMobEffect, 0.5f);
        
        Destroy(enemy, 0.4f);
    }
    
    private void EnableMovement() {
        
        GetComponent<PlayerController>()._canMove = true;
    }

    IEnumerator BlinkAndResetInvincible() {
        
        for (int i = 0; i < 20; i++) {
            _renderer.enabled = ! _renderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        
        _renderer.material.color = _intialColor;
        _renderer.enabled = true;
        _IsInvincible = false;
    }
}
