using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffWeightForItem : MonoBehaviour
{
    public int weightBuff;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.GetComponent<Item_BaseSet>().ChangeWeight(20);
        }
    }
}
