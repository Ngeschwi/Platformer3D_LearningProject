using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAnim : MonoBehaviour
{
    public Vector3 _amount;
    public float _time;
    void Start() {

        float _randomTine = Random.Range(_time - 0.3f, _time + 0.3f);
        
        iTween.PunchScale(gameObject, iTween.Hash(
            "amount", _amount,
            "time", _randomTine,
            "looptype", iTween.LoopType.loop
        ));
    }
}
