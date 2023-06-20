using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CageScript : MonoBehaviour
{
    public static CageScript _InstanceCageScript;
    
    public GameObject _CageText;
    public GameObject _OpeningCageEffect;
    private bool _canOpen = false;
    private GameObject _WholeCage;
    private GameObject _Cage;
    private GameObject _Message;

    private void Awake() {
        
        _InstanceCageScript = this;
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.E) && _canOpen)
            DestroyCage();
    }
    
    public void EnterInACageZone(Collider other) {
        
        _CageText.SetActive(true);
        _canOpen = true;
        _WholeCage = other.gameObject;
    }
    
    public void ExitInACageZone() {
        
        _CageText.SetActive(false);
        _canOpen = false;
        _WholeCage = null;
    }

    private void DestroyCage() {

        GetAllInfos();
        
        ObjectivesScript._InstanceObjectivesScript.UpdateFriends();

        _CageText.SetActive(false);
        _canOpen = false;
        
        iTween.ShakeRotation(_Cage, iTween.Hash(
            "amount", new Vector3(5f, 5f, 5f),
            "time", 1f,
            "oncomplete", "DestroyCageEffect"));
        
        Destroy(_Cage, 1.1f);
        
        _Message.SetActive(true);
        
        GameObject openingCageEffect = Instantiate(_OpeningCageEffect, _Cage.transform.position, Quaternion.identity);
        Destroy(openingCageEffect, 0.5f);
        
        _WholeCage.GetComponent<SphereCollider>().enabled = false;
    }

    private void DestroyCageEffect() {
        
        print("coucou");
        //transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        //
        //GameObject openingCageEffect = Instantiate(_OpeningCageEffect, _Cage.transform.position, Quaternion.identity);
        //Destroy(openingCageEffect, 0.5f);
    }

    private void GetAllInfos() {
        
        _Cage = _WholeCage.transform.GetChild(1).gameObject;
        _Message = _WholeCage.transform.GetChild(0).transform.GetChild(1).gameObject;
    }
}
