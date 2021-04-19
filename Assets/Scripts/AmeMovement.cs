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
    private BoxCollider2D RollHitBox;
    private EdgeCollider2D FloorHitBox;
    private bool canDie, isDead;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        RollHitBox = transform.Find("RollHitbox").GetComponent<BoxCollider2D>();
        FloorHitBox = transform.Find("FloorHitBox").GetComponent<EdgeCollider2D>();
        RollHitBox.enabled = false;
        FloorHitBox.enabled = false;
        canDie = true;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Ground = Physics2D.Raycast(transform.position, Vector3.down, 1.40f, LayerMask.GetMask("Ground", "Platform"));
        Debug.DrawRay(transform.position, Vector3.down * 1.40f, Color.red);
        if (!Ground && !FloorHitBox.enabled && !isDead)
            FloorHitBox.enabled = true;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (Ground)
            {
                StartCoroutine(Jump());
                Ground = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && Ground)
        {
            StartCoroutine(Roll());
        }
        Animator.SetBool("Jump", !Ground);
        if (Ground && FloorHitBox.enabled)
              FloorHitBox.enabled = false;
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

    private IEnumerator Roll()
    {
        RollHitBox.enabled = true;
        canDie = false;

        SoundManager.PlaySound("roll");
        Animator.Play("ame_spin");
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed * RollForce, Rigidbody2D.velocity.y);
        Animator.SetFloat("Speed", Mathf.Abs(Horizontal * Speed + RollForce));

        float counter = 0;
        float waitTime = 0.6f;

        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        RollHitBox.enabled = false;
        canDie = true;

    }

    public IEnumerator Die()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        SoundManager.PlaySound("death");
        Animator.SetTrigger("Dead");
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
        GetComponent<CapsuleCollider2D>().enabled = false;

        float counter = 0;
        float waitTime = 2.6f; //Animator.GetCurrentAnimatorStateInfo(0).length;

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

        float counter = 0;
        float waitTime = Animator.GetCurrentAnimatorStateInfo(0).length;
        
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }
        SoundManager.PlaySound("jump");
        Animator.Play("Ame_jump");

        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((collision.gameObject.CompareTag("Monster") && canDie) || collision.gameObject.CompareTag("Bullet")) && !isDead)
        {
            if(FloorHitBox.enabled && transform.position.y >= collision.transform.position.y)
            {
                StartCoroutine(Jump());
            }
            else
            {
                canDie = false;
                isDead = true;
                StartCoroutine(Die());
            }
        }
    }
}
