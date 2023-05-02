using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    Rigidbody2D rigit;
    [SerializeField] float bulletSpeed;
    bool isFired;
    Vector2 dir;
    Vector2 newPos;
    private void Awake()
    {
        rigit = GetComponent<Rigidbody2D>();
        isFired = false;
    }

    private void Update()
    {
        if (isFired)
        {
            newPos = transform.position;
            newPos.x += dir.x * bulletSpeed * Time.deltaTime;
            newPos.y += dir.y * bulletSpeed * Time.deltaTime;
            transform.position = newPos;
        }
    }
    public void StartFire(Vector2 _dir)
    {
        if (_dir.magnitude > 1) _dir.Normalize();
        this.dir = _dir;
        isFired = true;
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Character_BaseSet>().TakeDame(-10, 1, dir);
            Destroy(gameObject);
        }
       
        
    }
}
