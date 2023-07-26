using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float attackSpeed = 2;
    public GameObject damage;
    public Animator animator;

    Vector3 respawnPos;
    Rigidbody rb;
    PlayerInput pi;
    PlayerStats ps;
    bool isHarvesting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pi = GetComponent<PlayerInput>();
        ps = GetComponent<PlayerStats>();
        respawnPos = transform.position;
    }

    void Update()
    {
        Vector2 input = pi.actions["Move"].ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(input.x, 0, input.y);

        rb.velocity = movementDirection * speed + new Vector3(0, rb.velocity.y, 0);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            rb.MoveRotation(targetRotation);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        animator.SetBool("isHarvesting", isHarvesting);
    }

    private void Death()
    {
        transform.position = respawnPos;
        if (ps is not null)
            ps.ClearStats();
    }

    private IEnumerator Damage()
    {
        isHarvesting = true;
        while (isHarvesting)
        {

            yield return new WaitForSeconds(attackSpeed);
            damage.SetActive(true);
            yield return new WaitForSeconds(.1f);
            damage.SetActive(false);
            isHarvesting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Death();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Harvestable") && !isHarvesting)
        {
            StartCoroutine(Damage());
        }
    }
}
