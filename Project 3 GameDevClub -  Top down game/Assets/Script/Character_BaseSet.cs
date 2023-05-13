using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Character_BaseSet : MonoBehaviour
{

    [SerializeField] int maxWeight;
    public int currWeight { get; private set; }
    public float speed;
    GameObject character_ui;
    Slider weight_slider;
    Text weight_text;
    Image portraitChar;
    Image portraitCharBorder;
    [HideInInspector] public float hor;
    [HideInInspector] public float ver;
    public int playerID;
    /* 1: awsd
    * 2: arrow
    */
    public int controlType;
    public int attackType;
    
    Animator anim;
    Rigidbody2D rigit;
    BoxCollider2D box;
    private void Awake()
    {
        speed = 1 / maxWeight * 200;
        currWeight = maxWeight;
        character_ui = transform.Find("Character_UI").gameObject;
        weight_slider = character_ui.transform.Find("weight_slider").gameObject.GetComponent<Slider>();
        weight_text = character_ui.transform.Find("weight_text").gameObject.GetComponent<Text>();
        portraitChar = character_ui.transform.Find("character_portrait").gameObject.GetComponent<Image>();
        portraitCharBorder = transform.Find("UI_Image").Find("character_portraitBorder").gameObject.GetComponent<Image>();
        weight_slider.maxValue = maxWeight;
        weight_slider.value = currWeight;
        weight_text.text = $"{currWeight}/{maxWeight}";

        //initial set of typy control
        controlType = 2;
        attackType = 2;

        anim = GetComponent<Animator>();
        rigit = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        Debug.Log(maxWeight.ToString());
        speed = 1f / maxWeight * 200f;
        Debug.Log(speed.ToString());
        CheckUISide();

    }
    private void Update()
    {
        hor = 0; ver = 0;
        //Check control type of character
        if (controlType == 1) //awsd type
        {
            if (Input.GetKey(KeyCode.D)) hor = 1;
            else if (Input.GetKey(KeyCode.A)) hor = -1;
            if (Input.GetKey(KeyCode.W)) ver = 1;
            else if (Input.GetKey(KeyCode.S)) ver = -1;
        }
       else //arrow type
        {
            if (Input.GetKey(KeyCode.RightArrow)) hor = 1;
            else if (Input.GetKey(KeyCode.LeftArrow)) hor = -1;
            if (Input.GetKey(KeyCode.UpArrow)) ver = 1;
            else if (Input.GetKey(KeyCode.DownArrow)) ver = -1;
        }
        anim.SetBool("isMoving", hor != 0 || ver != 0);
        if (hor != 0 || ver != 0) Moving();
    }
    
    public void ChangeWeight(int _weight)
    {
        if (_weight < 0)
        {
            currWeight = (currWeight + _weight < 0) ? 1 : (currWeight + _weight);
        }
        else
        {
            currWeight = (currWeight + _weight > maxWeight) ? maxWeight : (currWeight + _weight);
        }
        speed = 1f / maxWeight * 200;
        weight_slider.value = currWeight;
        weight_text.text = $"{currWeight}/{maxWeight}";
    }
    public void TakeDame(int _weight, float forcePush, Vector2 dir)
    {
        ChangeWeight(_weight);
        gameObject.GetComponent<Animator>().SetTrigger("hit");
        StartCoroutine(Repelling(forcePush, dir, 0.25f));
    }
    IEnumerator Repelling(float forcePush, Vector2 dir, float dur)
    {
        for (int i = 0; i < 1 ; i++)
        {
            rigit.AddForce(dir * forcePush * 100 / currWeight, ForceMode2D.Impulse);
            yield return new WaitForSeconds(dur);
        }
        rigit.AddForce(-dir * forcePush * 100 / currWeight, ForceMode2D.Impulse);

    }
    void Moving()
    {
        anim.SetFloat("dirX", hor);
        anim.SetFloat("dirY", ver);
        Vector2 newPos = new Vector2(hor, ver);
        if (newPos.magnitude > 1) newPos.Normalize();
        newPos *= speed * Time.deltaTime;
        rigit.position += newPos;
    }
    void CheckUISide()
    {
        Vector3 hold;
        if (controlType == 1)
        {

            if (portraitCharBorder.GetComponent<RectTransform>().anchoredPosition.x > 0)
            {
                hold = portraitCharBorder.GetComponent<RectTransform>().anchoredPosition;
                hold.x -= 985;
                portraitCharBorder.GetComponent<RectTransform>().anchoredPosition = hold;

                hold = portraitChar.GetComponent<RectTransform>().anchoredPosition;
                hold.x -= 985;
                portraitChar.GetComponent<RectTransform>().anchoredPosition = hold;

                hold = weight_slider.GetComponent<RectTransform>().anchoredPosition;
                hold.x -= 985;
                weight_slider.GetComponent<RectTransform>().anchoredPosition = hold;

                hold = weight_text.GetComponent<RectTransform>().anchoredPosition;
                hold.x -= 985;
                weight_text.GetComponent<RectTransform>().anchoredPosition = hold;
            }
        }
        else if (controlType == 2)
        {
            if (portraitCharBorder.GetComponent<RectTransform>().anchoredPosition.x < 0)
            {
                hold = portraitCharBorder.GetComponent<RectTransform>().anchoredPosition;
                hold.x += 985;
                portraitCharBorder.GetComponent<RectTransform>().anchoredPosition = hold;

                hold = portraitChar.GetComponent<RectTransform>().anchoredPosition;
                hold.x += 985;
                portraitChar.GetComponent<RectTransform>().anchoredPosition = hold;

                hold = weight_slider.GetComponent<RectTransform>().anchoredPosition;
                hold.x += 985;
                weight_slider.GetComponent<RectTransform>().anchoredPosition = hold;

                hold = weight_text.GetComponent<RectTransform>().anchoredPosition;
                hold.x += 985;
                weight_text.GetComponent<RectTransform>().anchoredPosition = hold;
            }
        }
    }
    
}
