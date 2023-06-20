using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesScript : MonoBehaviour
{
    public static ObjectivesScript _InstanceObjectivesScript;
    
    public int _nbrOfObjectivesCompleted = 0;
    private int _nbrOfFriends;
    private int _nbrOfEnemies;
    private int _nbrOfCoins;
    private int _nbrOfFriendsSaved = 0;
    private int _nbrOfEnemiesKilled = 0;
    private int _nbrOfCoinsCollected = 0;
    
    public Text _textObjectiveFriends;
    public Text _textObjectiveEnemies;
    public Text _textObjectiveCoins;
    
    public void Awake() {
        
        _InstanceObjectivesScript = this;
    }

    public void Start() {
        
        _nbrOfFriends = GameObject.FindGameObjectsWithTag("Cage").Length;
        _nbrOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _nbrOfCoins = GameObject.FindGameObjectsWithTag("Coin").Length + _nbrOfEnemies;
    }

    public void RefreshObjectives() {
        
        _textObjectiveFriends.text = "- Free all your friends : " + _nbrOfFriendsSaved + "/" + _nbrOfFriends;
        _textObjectiveEnemies.text = "- Kill all enemies : " + _nbrOfEnemiesKilled + "/" + _nbrOfEnemies;
        _textObjectiveCoins.text = "- Pickup all coins : " + _nbrOfCoinsCollected + "/" + _nbrOfCoins;
    }
    
    public void UpdateFriends() {
        
        _nbrOfFriendsSaved++;
        if (_nbrOfFriendsSaved == _nbrOfFriends) {
            _textObjectiveFriends.color = Color.green;
            _nbrOfObjectivesCompleted++;
        }
    }
    
    public void UpdateEnemies() {
        
        _nbrOfEnemiesKilled++;
        if (_nbrOfEnemiesKilled == _nbrOfEnemies) {
            _textObjectiveEnemies.color = Color.green;
            _nbrOfObjectivesCompleted++;
        }
    }
    
    public void UpdateCoins() {
        
        _nbrOfCoinsCollected++;
        if (_nbrOfCoinsCollected == _nbrOfCoins) {
            _textObjectiveCoins.color = Color.green;
            _nbrOfObjectivesCompleted++;
        }
    }
}
