using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Cowboy_attack : MonoBehaviour
{
    Vector3 bulletInitPos;
    [SerializeField] GameObject bullet;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("attack1");
        }
    }
    public void FireBullet()
    {
        if (anim.GetFloat("dirX") == 1)
        {
            if (anim.GetFloat("dirY") == 0)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_right").transform.position;
            }
            if (anim.GetFloat("dirY") == 1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_trenphai").transform.position;
            }
            if (anim.GetFloat("dirY") == -1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_duoiphai").transform.position;
            }
        }
        else if (anim.GetFloat("dirX") == -1)
        {
            if (anim.GetFloat("dirY") == 0)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_left").transform.position;
            }
            if (anim.GetFloat("dirY") == -1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_duoitrai").transform.position;
            }
            if (anim.GetFloat("dirY") == 1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_trentrai").transform.position;
            }
        }
        else
        {
            if (anim.GetFloat("dirY") == 0)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_left").transform.position;
            }
            if (anim.GetFloat("dirY") == -1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_down").transform.position;
            }
            if (anim.GetFloat("dirY") == 1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_top").transform.position;
            }
        }
        Debug.Log(bulletInitPos.ToString());
         GameObject bulletClone = Instantiate(bullet, bulletInitPos, quaternion.identity);
        bulletClone.GetComponent<BulletScript>().StartFire(new Vector2(anim.GetFloat("dirX"), anim.GetFloat("dirY")));
    }
}

