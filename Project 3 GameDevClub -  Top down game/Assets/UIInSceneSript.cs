using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIInSceneSript : MonoBehaviour
{
    public static UIInSceneSript instance { get; private set; }
    Text txtCharacterWin;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        txtCharacterWin = transform.Find("UI_Image").transform.Find("txtCharacterWin").gameObject.GetComponent<Text>();
        txtCharacterWin.gameObject.SetActive(false);
    }
    public void PrintCharacterWin(GameObject character)
    {
        txtCharacterWin.gameObject.SetActive(true);
        string _playerID = character.GetComponent<Character_BaseSet>().playerID == 1 ? "2" : "1";
        txtCharacterWin.text = $"Player {_playerID} win";
        StartCoroutine(GameManager.instance.TextAppear(txtCharacterWin, 80, 120));
    }
}
