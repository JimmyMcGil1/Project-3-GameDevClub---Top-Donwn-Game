using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashZoneScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FlashCharacter(collision.gameObject));
        }
    }
 
    IEnumerator FlashCharacter(GameObject character)
    {
        Animator anim;
        anim = character.GetComponent<Animator>();
        Vector2 _dir = new Vector2(anim.GetFloat("dirX"), anim.GetFloat("dirY"));
        for (int i = 0; i < 1; i++)
        {
            character.GetComponent<Rigidbody2D>().AddForce(1f * _dir, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
        }
        character.GetComponent<Rigidbody2D>().AddForce(-1f * _dir, ForceMode2D.Impulse);

    }
}
