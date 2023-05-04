using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SanDau_Beheviour : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           // collision.GetComponent<Rigidbody2D>().gravityScale = 9;
           // collision.GetComponent<SpriteRenderer>().sortingOrder -= 1;
        }
    }
    private void OnTriggeStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is standing on san dau");
        }
    }
}
