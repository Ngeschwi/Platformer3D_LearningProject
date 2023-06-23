using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController _InstanceCameraController;
    
    public Transform _target;
    public Vector3 _offset;
    
    private bool _isFollowing = true;

    void Awake() {
        
        _InstanceCameraController = this;
    }
    
    void Start() {
        
        _offset = _target.position - transform.position;
    }

    void Update() {
        
        if (_isFollowing) {
            transform.position = _target.position - _offset;
        }
    }
    
    public void RotateCamera(float angle) {
        
        transform.RotateAround(_target.position, Vector3.up, angle);
        _offset = _target.position - transform.position;
        
        StartCoroutine("WaitForRotate");
    }
    
    private IEnumerator WaitForRotate() {
        
        _isFollowing = false;
        PlayerController._InstancePlayerController._canMove = false;
        yield return new WaitForSeconds(1.5f);
        PlayerController._InstancePlayerController._canMove = true;
        _isFollowing = true;
    }
}
