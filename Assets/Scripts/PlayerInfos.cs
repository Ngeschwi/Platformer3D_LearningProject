using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfos : MonoBehaviour
{
    public static PlayerInfos _InstancePlayerInfos;
    
    public GameObject _Player;
    public int _PlayerHealth = 3;
    public int _MaxHealth = 3;
    public int _PlayerCoins = 0;
    public Image[] _heartImages;
    public Text _coinText;
    public Text _finalScoreText;
    public GameObject _finalScorePanel;

    private void Awake() {

        _InstancePlayerInfos = this;
    }

    public void SetHealth(int health) {
        
        _PlayerHealth += health;
        if (_PlayerHealth > _MaxHealth)
            _PlayerHealth = _MaxHealth;
        else if (_PlayerHealth <= 0)
            CheckPointManager._InstanceCheckPointManager.RespawnPlayer();

        SetHealthBar();
    }

    public void GetCoin() {
        
        _PlayerCoins++;
        _coinText.text = _PlayerCoins.ToString();
    }
    
    public void SetHealthBar() {

        foreach (Image heart in _heartImages) {
            heart.enabled = false;
        }
        
        for (int i = 0; i < _PlayerHealth; i++) {
            _heartImages[i].enabled = true;
        }
    }
    
    public int GetScore() {

        int scoreFinal = _PlayerCoins * 5 + _PlayerHealth * 10;
        return scoreFinal;
    }
    
    public void ShowFinalScore() {

        _finalScoreText.text = GetScore().ToString();
        _finalScorePanel.SetActive(true);
    }
}
