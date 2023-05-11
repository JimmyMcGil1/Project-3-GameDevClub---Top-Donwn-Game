using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyBeheviour : MonoBehaviour
{
    public static CowboyBeheviour instance { get; private set; }
    Animator anim;
    Rigidbody2D rigit;
    [HideInInspector] public float hor;
    [HideInInspector] public float ver;
    Character_BaseSet baseSet;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        anim = GetComponent<Animator>();
        rigit = GetComponent<Rigidbody2D>();
        baseSet = GetComponent<Character_BaseSet>();
    }
    private void Update()
    {
        hor = 0; ver = 0;
        if (Input.GetKey(KeyCode.D)) hor = 1;
        else if (Input.GetKey(KeyCode.A)) hor = -1;
        if (Input.GetKey(KeyCode.W)) ver = 1;
        else if (Input.GetKey(KeyCode.S)) ver = -1;
        anim.SetBool("isMoving", hor != 0 || ver != 0);
        if (hor != 0 || ver != 0) Moving();
    }

    void Moving()
    {
        anim.SetFloat("dirX", hor);
        anim.SetFloat("dirY", ver);
        Vector2 newPos = new Vector2(hor, ver);
        if (newPos.magnitude > 1) newPos.Normalize();
        newPos *= baseSet.speed * Time.deltaTime;
        rigit.position += newPos;
    }




    
}
