using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanDauSeeSaw : MonoBehaviour
{
    public float rotationSpeed = 1f;
    Vector3 customAxis = new Vector3(0, 1f, 0f);
    GameObject[] players;
    float balancer = 0;
    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E)) LechGocDuoiTrai();
        if (Input.GetKey(KeyCode.R)) LechGocDuoiPhai();
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
        /*
        Quaternion currRotation = transform.rotation;
        Quaternion newRotation = Quaternion.Euler(currRotation.eulerAngles += new Vector3(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, 0f));
        transform.rotation = newRotation;
        transform.Rotate
        */
        Quaternion customRotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, customAxis);
        balancer += -rotationSpeed * Time.deltaTime;
        transform.Rotate(customRotation.eulerAngles);
       // StartCoroutine(DragCharacter(Vector3.left));
        
    }
    void LechGocDuoiPhai()
    {
        /*
        Quaternion currRotation = transform.rotation;
        Quaternion newRotation = Quaternion.Euler(currRotation.eulerAngles += new Vector3(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, 0f));
        transform.rotation = newRotation;
        transform.Rotate
        */
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
                player.GetComponent<Rigidbody2D>().AddForce(0.01f * dir, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(0.5f);
        }
        /*
        foreach (var player in players)
        {
            player.GetComponent<Rigidbody2D>().AddForce(1 * -dir, ForceMode2D.Impulse);
        }
        */
    }
}