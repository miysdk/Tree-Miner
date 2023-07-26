using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float attackSpeed = 2;
    public GameObject damage;
    public Animator animator;

    Rigidbody rb;
    PlayerInput pi;
    public bool isHarvesting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pi = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Vector2 input = pi.actions["Move"].ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(input.x, 0f, input.y);

        rb.velocity = movementDirection * speed;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Harvestable") && !isHarvesting)
        {
            StartCoroutine(Damage());
        }
    }
}
