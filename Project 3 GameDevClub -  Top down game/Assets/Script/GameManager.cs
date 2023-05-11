
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] List<GameObject> CharacterPool = new List<GameObject>();
    int characterPlayer1Select;
    int characterPlayer2Select;
    Transform initPlayer1;
    Transform initPlayer2;
    bool ldChar;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        initPlayer1 = transform.Find("InitPosPlayer1").gameObject.transform;
        initPlayer2 = transform.Find("InitPosPlayer2").gameObject.transform;
        DontDestroyOnLoad(gameObject.transform.parent);
        ldChar = false;
    }
    public void StartGame()
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
        ldChar = true;
        
    }
   
    public void GameOver(GameObject CharaccterWin)
    {
        Time.timeScale = 0;
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
    public void ReceivePlayerSelect(int playerId,int characterNumber)
    {
        Debug.Log(playerId.ToString() + " " +  characterNumber.ToString());
        if (playerId == 1) characterPlayer1Select = characterNumber;
        else if (playerId == 2) characterPlayer2Select = characterNumber; 
    }
    private void Update()
    {
     
    }
}

 