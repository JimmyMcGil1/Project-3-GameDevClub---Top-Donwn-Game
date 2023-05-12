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
        
    }

    




    
}
