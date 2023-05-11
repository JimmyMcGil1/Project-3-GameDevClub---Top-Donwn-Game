using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char4_behavior : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    float hor;
    float ver;
    Character_BaseSet baseSet;

    [Header ("Attack")]
    [SerializeField] float attackTimmer;
    float attackCounter;
    BoxCollider2D box;
    [SerializeField] float attackRadius;
    [SerializeField] float attackPos;
    [SerializeField] LayerMask playerLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        baseSet = GetComponent<Character_BaseSet>();
        attackCounter = Mathf.Infinity;
        box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        anim.SetFloat("dirX", -1);
        anim.SetFloat("dirY", 0);
    }

    private void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        anim.SetBool("isMoving", hor != 0 || ver != 0);
        if (hor != 0 || ver != 0) Moving();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (attackCounter > attackTimmer)
            {
                anim.SetTrigger("attack");
                attackCounter = 0;
            }
        }
        attackCounter += Time.deltaTime;
    }

    void Moving()
    {
        anim.SetFloat("dirX", hor);
        anim.SetFloat("dirY", ver);
        Vector2 newPos = new Vector2(hor, ver);
        if (newPos.magnitude > 1) newPos.Normalize();
        newPos *= baseSet.speed * Time.deltaTime;
        rigid.position += newPos;
    }

    void Attack()
    {
        Vector2 _attackPos = box.bounds.center + new Vector3(anim.GetFloat("dirX"), anim.GetFloat("dirY")) * attackPos;
        RaycastHit2D hit = Physics2D.CircleCast(_attackPos, attackRadius, new Vector3(anim.GetFloat("dirX"), anim.GetFloat("dirY")), 0.01f, playerLayer );
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 8)
            {
                Debug.Log("Hit enemy");
                StartCoroutine(AttackPush(hit.collider.gameObject.transform.parent.gameObject));
            }
        }   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        if (!Application.isPlaying) return;
        Vector2 _attackPos = box.bounds.center + new Vector3(anim.GetFloat("dirX"), anim.GetFloat("dirY")) * attackPos;
        Gizmos.DrawWireSphere(_attackPos, attackRadius);
    }

    IEnumerator AttackPush(GameObject other)
    {
        for (int i = 0; i < 1; i++)
        {
            other.GetComponent<Rigidbody2D>().AddForce(new Vector3(anim.GetFloat("dirX"), anim.GetFloat("dirY")) * 1.3f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
        }
        other.GetComponent<Rigidbody2D>().AddForce(new Vector3(anim.GetFloat("dirX"), anim.GetFloat("dirY")) * -1.3f, ForceMode2D.Impulse);
    }
}

