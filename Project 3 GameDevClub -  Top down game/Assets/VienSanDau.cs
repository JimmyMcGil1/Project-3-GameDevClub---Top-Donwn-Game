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
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            UIInSceneSript.instance.PrintCharacterWin(collision.gameObject);
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2;
            StartCoroutine(GameOverInvoker(1,collision.gameObject));
            
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 9;
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

        }
        
    }
    IEnumerator GameOverInvoker(float sec, GameObject _collision)
    {
        for (int i = 0; i < sec; i++)
        {
            yield return new WaitForSeconds(sec);
        }
        GameManager.instance.GameOver(_collision.gameObject);
    }
}
