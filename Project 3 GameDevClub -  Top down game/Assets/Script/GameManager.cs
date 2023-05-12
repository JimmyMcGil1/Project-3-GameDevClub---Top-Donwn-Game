
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] List<GameObject> CharacterPool = new List<GameObject>();
    int characterPlayer1Select;
    int characterPlayer2Select;
    Transform initPlayer1;
    Transform initPlayer2;
     public GameObject loadChar;
    public int battleDuration;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        initPlayer1 = transform.Find("InitPosPlayer1").gameObject.transform;
        initPlayer2 = transform.Find("InitPosPlayer2").gameObject.transform;
        GameObject obj = GameObject.FindGameObjectWithTag("GameController");
        if (obj == null)
        {
            DontDestroyOnLoad(gameObject.transform.parent);
        }
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("SelectCharScene");
    }
    public void Restart()
    {
        SceneManager.LoadScene("SelectCharScene");
    }
    public void ExitGame()
    {
        Application.Quit();
       
    }
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("SampleScene");
        loadChar = gameObject.transform.parent.Find("LoadCharacter").gameObject;
        loadChar.GetComponent<LoadCharacterScript>();
    }

    public void GameOver(GameObject CharaccterWin)
    {
        Time.timeScale = 0;
        UIInSceneSript.instance.DisplayUIButton();
    }

    public IEnumerator TextAppear(Text text, int fromSize, int toSize)
    {
        text.fontSize = fromSize;
        for (int i = 0; i < toSize - fromSize; i++)
        {
            text.fontSize += 1;
            yield return new WaitForSeconds(0.01f);
        }
    }
   
    private void Update()
    {
     
    }
}

 