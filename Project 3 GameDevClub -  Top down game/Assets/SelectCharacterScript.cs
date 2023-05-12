
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacterScript : MonoBehaviour
{
    /*player 1 select character by awsd, 
     * player 2 by arrow 
     */
    GameObject cursor1;
    GameObject cursor2;
    int checkMoveCur1 = 0;
    int checkMoveCur2 = 0;
    int cur1Id = 1; 
    int cur2Id = 2; 
    Text txtPlayer1;
    Text txtPlayer2;
    [SerializeField] string character1_name;
    [SerializeField] string character2_name;
    [SerializeField] string character3_name;
    GameObject loadChar;
    private void Awake()
    {
        cursor1 = transform.Find("Cursor1").gameObject;
        cursor2 = transform.Find("Cursor2").gameObject;
        txtPlayer1 = transform.Find("txtPlayer1").gameObject.GetComponent<Text>();
        txtPlayer2 = transform.Find("txtPlayer2").gameObject.GetComponent<Text>();
        loadChar = GameObject.FindGameObjectWithTag("LoadCharacter").gameObject;
        loadChar.SetActive(false);
    }
    private void Start()
    {
        txtPlayer1.text = character1_name;
        txtPlayer2.text = character1_name;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) CursorMoveLeft(ref checkMoveCur1, cur1Id);
        if (Input.GetKeyDown(KeyCode.D)) CursorMoveRight(ref checkMoveCur1,cur1Id);

        if (Input.GetKeyDown(KeyCode.LeftArrow)) CursorMoveLeft(ref checkMoveCur2,cur2Id);
        if (Input.GetKeyDown(KeyCode.RightArrow)) CursorMoveRight(ref checkMoveCur2,cur2Id);
        if (Input.GetKeyDown(KeyCode.C)) NewReceiveCharacter(cur1Id, checkMoveCur1);
        if (Input.GetKeyDown(KeyCode.Return)) NewReceiveCharacter(cur2Id, checkMoveCur2);
    }
    void CursorMoveLeft(ref int checkMove, int _curID = 1)
    {
        if (checkMove > 0)
        {
            Vector2 newPos;
            if (_curID == 1)  newPos = cursor1.GetComponent<RectTransform>().anchoredPosition;
            else newPos = cursor2.GetComponent<RectTransform>().anchoredPosition;
            Debug.Log(newPos.ToString());
            newPos.x -= 300;
            if (_curID == 1) cursor1.GetComponent<RectTransform>().anchoredPosition = newPos;
            else cursor2.GetComponent<RectTransform>().anchoredPosition = newPos;
            checkMove -= 1;
        }
       if (_curID == 1) ChangeCharacterName(txtPlayer1, checkMove);
       else ChangeCharacterName(txtPlayer2, checkMove);
    } 
    void CursorMoveRight(ref int checkMove, int _curID = 1)
    {
        if (checkMove < 2)
        {
            Vector2 newPos;
            if (_curID == 1) newPos = cursor1.GetComponent<RectTransform>().anchoredPosition;
            else newPos = cursor2.GetComponent<RectTransform>().anchoredPosition;
            Debug.Log(newPos.ToString());
            newPos.x += 300;
            if (_curID == 1) cursor1.GetComponent<RectTransform>().anchoredPosition = newPos;
            else cursor2.GetComponent<RectTransform>().anchoredPosition = newPos;
            checkMove += 1;
        }
        if (_curID == 1) ChangeCharacterName(txtPlayer1, checkMove);
        else ChangeCharacterName(txtPlayer2, checkMove);
    }
    void ChangeCharacterName(Text text, int checkMove)
    {
        switch (checkMove)
        {
            case 0:
                text.text = character1_name;
                break;
            case 1:
                text.text = character2_name;
                break;
            case 2:
                text.text = character3_name;
                break;
            default:
                break;
        }
    }
    void NewReceiveCharacter(int playerID, int slChar)
    {
        Debug.Log(playerID.ToString() + " " + slChar.ToString());
        if (playerID == 1)
            PlayerPrefs.SetInt("player1SlChar", slChar);
        else if (playerID == 2)
            PlayerPrefs.SetInt("player2SlChar", slChar);
    }
    public void LoadBattleScene_SelectScene()
    {
        loadChar.SetActive(true);
        GameManager.instance.LoadBattleScene();
    }
}
