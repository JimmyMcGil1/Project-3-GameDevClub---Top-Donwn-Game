using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacterScript : MonoBehaviour
{
    [SerializeField] Transform initPlayer1;
    [SerializeField] Transform initPlayer2;
    public GameObject[] CharacterPool;
    private void Start()
    {
        int player1SlChar = PlayerPrefs.GetInt("player1SlChar");
        int player2SlChar = PlayerPrefs.GetInt("player2SlChar");
         Vector3 fixInit1 = initPlayer1.position;
         Vector3 fixInit2 = initPlayer2.position;
        fixInit1.z = 0;
        fixInit2.z = 0;
        initPlayer1.position = fixInit1;
        initPlayer2.position = fixInit2;
        Instantiate(CharacterPool[player1SlChar], initPlayer1.position, Quaternion.Euler(0,0,0));
        Instantiate(CharacterPool[player2SlChar], initPlayer2.position, Quaternion.Euler(0, 0, 0));
    }
}
