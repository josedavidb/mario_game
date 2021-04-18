using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmeMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Ground;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Ground = Physics2D.Raycast(transform.position, Vector3.down, 1.40f);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (Ground)
            {
                Jump();
                Ground = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Rigidbody2D.AddForce(Vector2.down * JumpForce);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Animator.SetTrigger("Attack");
            Roll();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Die();
        }
        Animator.SetBool("Jump", !Ground);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
        Animator.SetFloat("Speed", Mathf.Abs(Horizontal * Speed));
        float hSpeed = Input.GetAxis("Horizontal");

        if (hSpeed > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (hSpeed < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Roll()
    {
        Rigidbody2D.AddForce(Vector2.right * 10);
    }

    public void DieAnimation()
    {
        //Animator.Play("Ame_dead");
        //if (Animator.)

    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
