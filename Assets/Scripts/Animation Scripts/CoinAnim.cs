using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnim : MonoBehaviour
{
    public Vector3 _dir;
    public float _translateSpeed;
    public float _translateHeight;
    private float _initPositionY;
    
    void Start() {
        _initPositionY = transform.position.y;
    }
    
    void Update() {
        
        transform.Rotate(_dir * Time.deltaTime);
        
        transform.position = new Vector3(
            transform.position.x,
            _initPositionY + Mathf.Sin(Time.time * _translateSpeed) * _translateHeight,
            transform.position.z);
    }
}
