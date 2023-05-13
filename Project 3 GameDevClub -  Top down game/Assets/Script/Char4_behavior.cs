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
    [SerializeField] Vector3 allignBoxPos;
    [SerializeField] LayerMask playerLayer;

    [Header("Skill 2")]
    [SerializeField] float skill2Duration;
    [SerializeField] float skill2Weight;
    [SerializeField] float skill2AnimationDuration;
    float skill2Counter;
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && baseSet.attackType == 2)
        {
            if (attackCounter > attackTimmer)
            {
                anim.SetTrigger("attack");
                attackCounter = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && baseSet.attackType == 1)
        {
            if (attackCounter > attackTimmer)
            {
                anim.SetTrigger("attack");
                attackCounter = 0;
            }
        }
        attackCounter += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.O) && baseSet.attackType == 1)
        {
            anim.SetTrigger("skill2");
            skill2Counter = skill2Duration;
            baseSet.ChangeWeight(Mathf.RoundToInt(skill2Weight));
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && baseSet.attackType == 2)
        {
            anim.SetTrigger("skill2");
            skill2Counter = skill2Duration;
            baseSet.ChangeWeight(Mathf.RoundToInt(skill2Weight));
        }

        if (skill2Counter > 0)
        {
            skill2Counter -= Time.deltaTime;
            if (skill2Counter <= 0)
            {
                baseSet.ChangeWeight(-Mathf.RoundToInt(skill2Weight));
            }
        }
    }

   
    void Attack()
    {
        Vector2 _attackPos = box.bounds.center + allignBoxPos + new Vector3(anim.GetFloat("dirX"), anim.GetFloat("dirY")) * attackPos;
        RaycastHit2D hit = Physics2D.CircleCast(_attackPos, attackRadius, new Vector3(anim.GetFloat("dirX"), anim.GetFloat("dirY")), 0.01f, playerLayer );
        if (hit.collider != null)
        {
            GameObject myseft = gameObject.transform.Find("char4Collider").gameObject;
            if (hit.collider.gameObject.layer == 8 && hit.collider.gameObject != myseft)
            {
                Debug.Log("Hit enemy");

                Vector2 dir = new Vector2(anim.GetFloat("dirX"), anim.GetFloat("dirY"));
                hit.collider.transform.parent.GetComponent<Character_BaseSet>().TakeDame(-10, 1.3f, dir);
            }
        }   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //if (!Application.isPlaying) return;
        Vector2 _attackPos = box.bounds.center + allignBoxPos + new Vector3(anim.GetFloat("dirX"), anim.GetFloat("dirY")) * attackPos;
        Gizmos.DrawWireSphere(_attackPos, attackRadius);
    }

    private void Skill2()
    {
        StartCoroutine(Skill2Animation(skill2AnimationDuration));
        baseSet.ChangeWeight(Mathf.RoundToInt(skill2Weight));
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

    IEnumerator Skill2Animation(float duration)
    {
        anim.SetTrigger("skill2");
        yield return new WaitForSeconds(duration);
        anim.ResetTrigger("skill2");
        baseSet.ChangeWeight(-Mathf.RoundToInt(skill2Weight));
        skill2Counter = skill2Duration;
    }
}

