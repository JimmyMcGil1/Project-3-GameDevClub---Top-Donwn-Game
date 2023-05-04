using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VienSanDau : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 9;
        }
    }
}
