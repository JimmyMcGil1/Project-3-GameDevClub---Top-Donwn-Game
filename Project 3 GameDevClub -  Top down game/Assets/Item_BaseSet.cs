using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BaseSet : MonoBehaviour
{
    [SerializeField] public int weight { get;  set; }
    private void Awake()
    {
        weight = 0;
    }
}
