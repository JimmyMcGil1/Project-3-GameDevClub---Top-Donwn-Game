using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    Rigidbody2D rigit;
    [SerializeField] float bulletSpeed;
    bool isFired;
    private void Awake()
    {
        rigit = GetComponent<Rigidbody2D>();
        isFired = false;
    }
   
    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        
    }
    public void StartFire(Vector2 dir)
    {
        if (dir.magnitude > 1)  dir.Normalize();
        Debug.Log(dir.ToString());
        rigit.velocity = new Vector2(dir.x, dir.y) * bulletSpeed;

    }
}
