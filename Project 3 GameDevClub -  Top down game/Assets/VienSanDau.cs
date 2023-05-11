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

            StartCoroutine(GameOverInvoker(1));
        }
        IEnumerator GameOverInvoker(float sec)
        {
            for (int i = 0; i < sec; i++)
            {
                yield return new WaitForSeconds(sec);
            }
            GameManager.instance.GameOver(collision.gameObject);
        }
    }
    
}
