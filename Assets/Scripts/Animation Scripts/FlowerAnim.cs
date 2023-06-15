using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnim : MonoBehaviour
{
    public Vector3 _amount;
    public float _time;
    void Start() {
        float _randomTime = Random.Range(_time - 1.5f, _time + 1.5f);
        
        iTween.PunchRotation(gameObject, iTween.Hash(
            "amount", _amount,
            "time", _randomTime,
            "looptype", iTween.LoopType.loop
        ));
    }
}
