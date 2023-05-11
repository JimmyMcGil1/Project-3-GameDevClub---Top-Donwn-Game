
using UnityEngine;
using UnityEngine.UI;

public class Item_BaseSet : MonoBehaviour
{
    [SerializeField] public int weight { get;  set; }
    [SerializeField] Text txt_weight;
     void Awake()
    {
        weight = 0;

    }
    private void Start()
    {
      //  Text txt_weight = transform.Find("UI_Cube").Find("txtWeightOfCube").gameObject.GetComponent<Text>();
        txt_weight.text = weight.ToString();
    }
    public void ChangeWeight(int _weight)
    {
            weight += _weight;
        txt_weight.text = $"{weight}";
    }
}
