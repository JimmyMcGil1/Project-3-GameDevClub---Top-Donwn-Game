using System.Collections;
using UnityEngine;

public class Char3_behiviour : MonoBehaviour
{
    Animator anim;
   

    [Header ("Attack")]
    [SerializeField] float attackTimmer;
    float attackCounter;
    BoxCollider2D box;
    [SerializeField] float attackRadius;
    [SerializeField] float attackPos;
    [SerializeField] LayerMask playerLayer;
    Character_BaseSet baseSet;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        attackCounter = Mathf.Infinity;
        baseSet = GetComponent<Character_BaseSet>();
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
        else if (Input.GetKeyDown(KeyCode.E) && baseSet.attackType == 1)
        {
            if (attackCounter > attackTimmer)
            {
                anim.SetTrigger("attack");
                attackCounter = 0;
            }
        }
        attackCounter += Time.deltaTime;
    }

   
    void Attack()
    {
        Vector2 _attackPos = box.bounds.center + new Vector3(anim.GetFloat("dirX"), anim.GetFloat("dirY")) * attackPos;
        RaycastHit2D hit = Physics2D.CircleCast(_attackPos, attackRadius, new Vector3(anim.GetFloat("dirX"), anim.GetFloat("dirY")), 0.01f, playerLayer );
        if (hit.collider != null)
        {
            //Layer is PlayerCollider
            if (hit.collider.gameObject.layer == 8)
            {
             //   StartCoroutine(AttackPush(hit.collider.gameObject.transform.parent.gameObject));
                Vector2 dir = new Vector2(anim.GetFloat("dirX"), anim.GetFloat("dirY"));
                hit.collider.transform.parent.GetComponent<Character_BaseSet>().TakeDame(-10, 1.3f, dir);
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
