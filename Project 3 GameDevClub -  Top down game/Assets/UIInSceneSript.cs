using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIInSceneSript : MonoBehaviour
{
    public static UIInSceneSript instance { get; private set; }
    Text txtCharacterWin;
    Slider sliderClock;
    Text txtClock;
    Text txtCountDown;
    Canvas UI_btns;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        txtCharacterWin = transform.Find("UI_Image").transform.Find("txtCharacterWin").gameObject.GetComponent<Text>();
        txtCountDown    = transform.Find("UI_Image").transform.Find("txtCountDownBegin").gameObject.GetComponent<Text>();
        sliderClock = transform.Find("UI_Image").transform.Find("SliderTimmer").gameObject.GetComponent<Slider>();
        txtClock = transform.Find("UI_Image").transform.Find("txtClock").gameObject.GetComponent<Text>();
        UI_btns = transform.Find("UI_ButtonInScene").gameObject.GetComponent<Canvas>();
        UI_btns.gameObject.SetActive(false);
        txtCharacterWin.gameObject.SetActive(false);
        sliderClock.maxValue = GameManager.instance.battleDuration; 
        sliderClock.value = GameManager.instance.battleDuration;
        txtClock.text = $"{sliderClock.value}";

    }
    private void Start()
    {
        StartCoroutine(ClockCoundown((int)sliderClock.maxValue));
        StartCoroutine(CountDownToBegin());
    }
    public void PrintCharacterWin(GameObject character)
    {
        txtCharacterWin.gameObject.SetActive(true);
        string _playerID = character.GetComponent<Character_BaseSet>().playerID == 1 ? "2" : "1";
        txtCharacterWin.text = $"Player {_playerID} win";
        if (_playerID == "2") txtCharacterWin.color = Color.yellow;
        else txtCharacterWin.color = Color.green;
        StartCoroutine(GameManager.instance.TextAppear(txtCharacterWin, 80, 120));
    }
    IEnumerator ClockCoundown(int sec)
    {
        for (int i = 0; i < sec; i++)
        {
            sliderClock.value -= 1;
            txtClock.text = $"{sliderClock.value}";
            yield return new WaitForSeconds(1);
        }
    }
    public void DisplayUIButton()
    {
        UI_btns.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    IEnumerator CountDownToBegin()
    {
        for (int i = 3; i >= 1; i--)
        {
            txtCountDown.text = $"{i}";
            GameManager.instance.TextAppear(txtCountDown, 80, 100);
            yield return new WaitForSeconds(1);
        }
        txtCountDown.gameObject.SetActive(false);
    }
}
