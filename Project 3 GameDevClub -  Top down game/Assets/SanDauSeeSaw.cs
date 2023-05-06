using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanDauSeeSaw : MonoBehaviour
{
    public float rotationSpeed = 1f;
    Vector3 customAxis = new Vector3(0, 1f, 0f);
    GameObject[] players;
    seesaw_sensor left;
    seesaw_sensor right;

    float balancer = 0;
    bool isPushCharacter = false;
    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        left = transform.Find("left sensor").gameObject.GetComponent<seesaw_sensor>();
        right = transform.Find("right sensor").gameObject.GetComponent<seesaw_sensor>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
           
            if (left.totalWeight < right.totalWeight )
            {
                if (balancer < 1) LechGocDuoiPhai();
            }

            else if (left.totalWeight > right.totalWeight) 
            { 
                if (balancer > -1) LechGocDuoiTrai();
            }
            else
            {
              if (balancer < 0) LechGocDuoiPhai();
              else if (balancer > 0) LechGocDuoiTrai();
             }
       
            
      
        if (balancer < 0)
        {
                StartCoroutine(DragCharacter(Vector3.left));
        }
        else if (balancer > 0)
        {
            StartCoroutine(DragCharacter(Vector3.right));
        }
    }
    void LechGocDuoiTrai()
    {
        
        Quaternion customRotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, customAxis);
        balancer += -rotationSpeed * Time.deltaTime;
        transform.Rotate(customRotation.eulerAngles);
       // StartCoroutine(DragCharacter(Vector3.left));
        
    }
    void LechGocDuoiPhai()
    {
        Quaternion customRotation = Quaternion.AngleAxis(-rotationSpeed * Time.deltaTime, customAxis);
        balancer += +rotationSpeed * Time.deltaTime;
        transform.Rotate(customRotation.eulerAngles);
        
        //   StartCoroutine(DragCharacter(Vector3.right));

    }
    IEnumerator DragCharacter(Vector3 dir)
    {
        for (int i = 0; i < 1; i++)
        {
            foreach (var player in players)
            {
                player.GetComponent<Rigidbody2D>().AddForce(0.005f * dir * player.GetComponent<Character_BaseSet>().currWeight / 100, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(1f);
        }
        foreach (var player in players)
        {
            player.GetComponent<Rigidbody2D>().AddForce(-0.005f * dir * player.GetComponent<Character_BaseSet>().currWeight / 100, ForceMode2D.Impulse);
        }
    }
}
