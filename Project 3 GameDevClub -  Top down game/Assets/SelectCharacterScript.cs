
using UnityEngine;
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
    private void Awake()
    {
        cursor1 = transform.Find("Cursor1").gameObject;
        cursor2 = transform.Find("Cursor2").gameObject;
        txtPlayer1 = transform.Find("txtPlayer1").gameObject.GetComponent<Text>();
        txtPlayer2 = transform.Find("txtPlayer2").gameObject.GetComponent<Text>();
    }
    private void Start()
    {
        txtPlayer1.text = character1_name;
        txtPlayer2.text = character1_name;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) CursorMoveLeft(cursor1,ref checkMoveCur1, cur1Id);
        if (Input.GetKeyDown(KeyCode.D)) CursorMoveRight(cursor1,ref checkMoveCur1,cur1Id);

        if (Input.GetKeyDown(KeyCode.LeftArrow)) CursorMoveLeft(cursor2, ref checkMoveCur2,cur2Id);
        if (Input.GetKeyDown(KeyCode.RightArrow)) CursorMoveRight(cursor2, ref checkMoveCur2,cur2Id);
        //   if (Input.GetKeyDown(KeyCode.C)) GameManager.instance.ReceivePlayerSelect(cur1Id, checkMoveCur1);
        //  if (Input.GetKeyDown(KeyCode.Return)) GameManager.instance.ReceivePlayerSelect(cur2Id, checkMoveCur2);
        if (Input.GetKeyDown(KeyCode.C)) NewReceiveCharacter(cur1Id, checkMoveCur1);
        if (Input.GetKeyDown(KeyCode.Return)) NewReceiveCharacter(cur2Id, checkMoveCur2);
    }
    void CursorMoveLeft(GameObject cur, ref int checkMove, int _curID)
    {
        if (checkMove > 0)
        {
            Vector2 newPos = cur.GetComponent<RectTransform>().position;
            newPos.x -= 300;
            cur.GetComponent<RectTransform>().position = newPos;
            checkMove -= 1;
        }
       if (_curID == 1) ChangeCharacterName(txtPlayer1, checkMove);
       else ChangeCharacterName(txtPlayer2, checkMove);
    } 
    void CursorMoveRight(GameObject cur, ref int checkMove, int _curID)
    {
        if (checkMove < 2)
        {
            Vector2 newPos = cur.GetComponent<RectTransform>().position;
            newPos.x += 300;
            cur.GetComponent<RectTransform>().position = newPos;
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
}
