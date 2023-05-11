
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene0");
    }
    public void ExitGame()
    {
        Application.Quit();
       
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
}

