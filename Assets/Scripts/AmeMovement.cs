using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmeMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float RollForce;

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
                StartCoroutine(Jump());
                Ground = false;
            }
        }
        //else if (Input.GetKeyDown(KeyCode.G))
        //{
        //    Rigidbody2D.AddForce(Vector2.down * JumpForce);
        //}
        else if (Input.GetKeyDown(KeyCode.R) && Ground)
        {
            Animator.SetTrigger("Attack");
            Roll();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(Die());
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

    //private void Jump()
    //{
    //    Rigidbody2D.AddForce(Vector2.up * JumpForce);
    //}

    private void Roll()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed+ RollForce, Rigidbody2D.velocity.y);
        Animator.SetFloat("Speed", Mathf.Abs(Horizontal * Speed + RollForce));
    }

    public void DieAnimation()
    {
        //Animator.Play("Ame_dead");
        //if (Animator.)

    }

    public IEnumerator Die()
    {
        Animator.SetTrigger("Dead");
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
        GetComponent<CapsuleCollider2D>().enabled = false;

        float counter = 0;
        float waitTime = 2.0f; //Animator.GetCurrentAnimatorStateInfo(0).length;

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator Jump()
    {
        Animator.Play("Ame_bounce");
        //Animator.CrossFadeInFixedTime("Ame_bounce", 0.6f);

        //Wait until we enter the current state
        //while (Animator.GetCurrentAnimatorStateInfo(0).IsName("Ame_bounce"))
        //{
        //    yield return null;
        //}

        float counter = 0;
        float waitTime = Animator.GetCurrentAnimatorStateInfo(0).length;
        
        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        Animator.Play("Ame_jump");

        Rigidbody2D.AddForce(Vector2.up * JumpForce);

    }
}
