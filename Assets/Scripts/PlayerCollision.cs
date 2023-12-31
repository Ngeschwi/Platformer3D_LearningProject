using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject _PickUpEffect;
    public GameObject _KillMobEffect;
    public GameObject _PlayerHitEffect;
    public GameObject _Coin;
    private Vector3 _HitDirection;
    private Vector3 _JumpDirection;
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
        
        if (other.gameObject.tag == "Coin")
            PickACoin(other);

        if (other.gameObject.tag == "Cam1")
            CameraController._InstanceCameraController.RotateCamera(-90);
        else if (other.gameObject.tag == "Cam2")
            CameraController._InstanceCameraController.RotateCamera(180);
            
        if (other.gameObject.tag == "Checkpoint")
            CheckPointManager._InstanceCheckPointManager.ActiveCheckPoint();
        
        if (other.gameObject.tag == "End")
            PlayerInfos._InstancePlayerInfos.ShowFinalScore();
        
        if (other.gameObject.tag == "Cage")
            CageScript._InstanceCageScript.EnterInACageZone(other);
    }

    private void OnTriggerExit(Collider other) {
        
        if (other.gameObject.tag == "Cam1")
            CameraController._InstanceCameraController.RotateCamera(90);
        else if (other.gameObject.tag == "Cam2")
            CameraController._InstanceCameraController.RotateCamera(180);
        
        
        if (other.gameObject.tag == "Cage")
            CageScript._InstanceCageScript.ExitInACageZone();
    }

    private void OnControllerColliderHit(ControllerColliderHit other) {
    
        if (other.gameObject.tag == "Hurt" && !_IsInvincible)
            StepBackWhenHit();
        else if (other.gameObject.tag == "Enemy")
            JumpOnEnemy(other.transform.parent.gameObject);
        
        if (other.gameObject.tag == "Fall")
            CheckPointManager._InstanceCheckPointManager.RespawnPlayer();
    }
    
    private void StepBackWhenHit() {
        
        PlayerInfos._InstancePlayerInfos.SetHealth(-1);
        
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
        
        Instantiate(_Coin, enemy.transform.position + Vector3.up + Vector3.forward * 2, Quaternion.identity * Quaternion.Euler(90f, 0, 0));
        
        ObjectivesScript._InstanceObjectivesScript.UpdateEnemies();
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

    private void PickACoin(Collider other) {
        
        _audioSource.PlayOneShot(_CoinsSound);
        GameObject go = Instantiate(_PickUpEffect, other.transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        Destroy(go, 0.5f);
        PlayerInfos._InstancePlayerInfos.GetCoin();
        
        ObjectivesScript._InstanceObjectivesScript.UpdateCoins();
    }
}
