using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector2 velocity;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(
            new Vector3(Random.Range(velocity.x, velocity.y), 5, Random.Range(velocity.x, velocity.y)),
            ForceMode.Impulse
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.instance.IncreaseLogs(1);
            Destroy(gameObject);
        }
    }
}
