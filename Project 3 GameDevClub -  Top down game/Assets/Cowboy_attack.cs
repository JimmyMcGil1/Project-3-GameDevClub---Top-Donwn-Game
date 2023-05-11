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
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("attack1");
        }
    }
    public void FireBullet()
    {
        Debug.Log("firebullet");
        float rotate = 0;
        if (anim.GetFloat("dirX") == 1)
        {
            if (anim.GetFloat("dirY") == 0)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_right").transform.position;
            }
            if (anim.GetFloat("dirY") == 1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_trenphai").transform.position;
                rotate += 45;
            }
            if (anim.GetFloat("dirY") == -1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_duoiphai").transform.position;
                rotate -= 45;
            }
        }
        else if (anim.GetFloat("dirX") == -1)
        {
            if (anim.GetFloat("dirY") == 0)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_left").transform.position;
                rotate += 180;

            }
            if (anim.GetFloat("dirY") == -1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_duoitrai").transform.position;
                rotate += 215;
            }
            if (anim.GetFloat("dirY") == 1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_trentrai").transform.position;
                rotate += 135;
            }
        }
        else
        {
            if (anim.GetFloat("dirY") == 0)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_left").transform.position;
                rotate += 180;
            }
            if (anim.GetFloat("dirY") == -1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_down").transform.position;
                rotate += 270;
            }
            if (anim.GetFloat("dirY") == 1)
            {
                bulletInitPos = gameObject.transform.Find("Bullet_InitPos_top").transform.position;
                rotate += 90;
            }
        }
        bullet.transform.eulerAngles = Vector3.forward * rotate;
         GameObject bulletClone = Instantiate(bullet, bulletInitPos, bullet.transform.rotation);
        bulletClone.GetComponent<BulletScript>().StartFire(new Vector2(anim.GetFloat("dirX"), anim.GetFloat("dirY")));
    }
}

