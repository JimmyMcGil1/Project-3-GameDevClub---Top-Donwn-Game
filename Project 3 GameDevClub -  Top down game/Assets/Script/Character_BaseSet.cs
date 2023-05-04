using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Character_BaseSet : MonoBehaviour
{

    [SerializeField] int maxWeight;
    public int currWeight { get; private set; }
    public float speed;
    Rigidbody2D rigit;
    BoxCollider2D box;
    GameObject character_ui;
    Slider weight_slider;
    Text weight_text;
    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        //speed = 1 / maxWeight * 200;
        currWeight = maxWeight;
        rigit = GetComponent<Rigidbody2D>();
       
        character_ui = transform.Find("Character_UI").gameObject;
        weight_slider = character_ui.transform.Find("weight_slider").gameObject.GetComponent<Slider>();
        weight_text = character_ui.transform.Find("weight_text").gameObject.GetComponent<Text>();
        weight_slider.maxValue = maxWeight;
        weight_slider.value = currWeight;
        weight_text.text = $"{currWeight}/{maxWeight}";
        box = GetComponent<BoxCollider2D>();
        }
    private void Start()
    {
        Debug.Log(maxWeight.ToString());
        speed = 1f / maxWeight * 200f;
        Debug.Log(speed.ToString());

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) ChangeWeight(-10);
        
    }
    void ChangeWeight(int _weight)
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
   
    
}
