using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager _InstanceCheckPointManager;
    
    private Material _CrystalMaterial;
    private Color _activeCrystalColor;
    public Vector3 _lastPosition;
    public GameObject _CheckpointHub;

    private void Awake() {
        
        _InstanceCheckPointManager = this;
    }

    void Start() {
        
        _lastPosition = transform.position;
        
        _CrystalMaterial = _CheckpointHub.transform.GetChild(0).GetComponent<Renderer>().material;
        _activeCrystalColor = _CrystalMaterial.GetColor("_EmissionColor");
        _CrystalMaterial.SetColor("_EmissionColor", Color.black);
    }
    
    public void ActiveCheckPoint() {
        
        _lastPosition = transform.position;
        _CrystalMaterial.SetColor("_EmissionColor", _activeCrystalColor);
        _CheckpointHub.transform.GetChild(0).gameObject.GetComponent<CoinAnim>().enabled = true;
    }
    
    public void RespawnPlayer() {

        print("RespawnPlayer");
        transform.position = _lastPosition;
        PlayerInfos._InstancePlayerInfos.SetHealth(PlayerInfos._InstancePlayerInfos._MaxHealth);
    }
}
