using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Sandau2Controller : MonoBehaviour
{
    public float g = 9.81f;
    public float forceMultiplier = 10f;
    public float rotationSpeed = 1f;
    public Tilemap tilemap;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float currentRotation = tilemap.transform.rotation.eulerAngles.z;

        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;
        tilemap.transform.rotation = Quaternion.Euler(20, 0f, newRotation); //tao do nghieng
    }

    private void FixedUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float inertiaForce = 0f;
        foreach (GameObject player in players)
        {
            if (player.layer == LayerMask.NameToLayer("Player"))
            {
                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                float weight = playerRb.mass;
                inertiaForce += weight * g;
            }
        }

        Tilemap tilemap = GetComponent<Tilemap>();
        foreach (var tilePos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(tilePos);
            if (tile != null)
            {
                Vector3Int tileLocalPos = tilemap.WorldToCell(tilemap.CellToWorld(tilePos));
                Vector2 tileWorldPos = tilemap.CellToWorld(tileLocalPos);

                Vector2 forceDir = (tileWorldPos - rb.worldCenterOfMass).normalized;
                float forceMag = forceMultiplier * inertiaForce;
                Vector2 force = forceDir * forceMag;

                rb.AddForceAtPosition(force, tileWorldPos);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector2 pushDir = collision.contacts[0].normal;
            float pushForce = 50f;
            rb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
        }
    }
}
