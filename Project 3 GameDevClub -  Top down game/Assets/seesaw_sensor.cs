using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seesaw_sensor : MonoBehaviour
{
    public int totalWeight { get; private set; }
    BoxCollider2D box;
    private void Awake()
    {
        totalWeight = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            totalWeight += collision.gameObject.GetComponent<Character_BaseSet>().currWeight;
        }    
        else if (collision.gameObject.CompareTag("Item"))
        {
            totalWeight += collision.gameObject.GetComponent<Item_BaseSet>().weight;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            totalWeight -= collision.gameObject.GetComponent<Character_BaseSet>().currWeight;

        }
        else if (collision.gameObject.CompareTag("Item"))
        {
            totalWeight -= collision.gameObject.GetComponent<Item_BaseSet>().weight;

        }
    }
}
