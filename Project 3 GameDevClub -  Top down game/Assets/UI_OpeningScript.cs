using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OpeningScript : MonoBehaviour
{
    GameObject loadChar;
    private void Awake()
    {
        loadChar = GameObject.FindGameObjectWithTag("LoadCharacter");
        loadChar.SetActive(false);
    }
}
