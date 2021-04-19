using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class RicardoController : MonoBehaviour
{
    public GameObject Ame;
    private float LastAction;
    private bool moveRight = false;
    private bool movement = false;
    private int speed = 0;
    private bool action = false;
    public Animator animator;
    private int Health = 3;

    void Start()
    {
    	animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = Ame.transform.position - transform.position;
        if (direction.x >= 0.0f) 
        {
        	transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
        	moveRight = true;
        } else 
        {
        	transform.localScale = new Vector3(-2.5f, 2.5f, 1.0f);
        	moveRight = false;
        }

        float distance = Mathf.Abs(Ame.transform.position.x - transform.position.x);

        if (distance < 30.0f && Time.time > LastAction + 3f && !action)
        {
        	Invoke("Action", 3);
        	LastAction = Time.time;
        }
        if (movement) Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {	
    	if (collision.gameObject.CompareTag("Player") && Health == 1 && !action)
    	{
    		animator.SetBool("Death", true);
    		action = true;
    		speed = 0;
    		Invoke("Death", 2);
    	}
    	if (collision.gameObject.CompareTag("Player") && !action){
    		Health -= 1;
    	}
    }

    private void Death()
    {
    	Destroy(gameObject);
    }

    public void Action()
    {
    	action = true;
    	System.Random random = new System.Random();
    	int num = random.Next(1,4);
    	if (num == 1) Walk();
    	//else if (num == 2) Shoot();
    	//else if (num == 3) Hit();
    }

    private void Shoot()
    {
    	Debug.Log("Shoot");
    	Invoke("Stop", 5);
    }

    private void Walk()
    {
    	StartMovement();
    	Invoke("Stop", 5);
    }

    private void StartMovement()
    {
    	movement = true;
    	animator.SetBool("Movement", movement);
    	speed = 7;
    }

    private void Movement()
    {
    	if (moveRight) {
    		transform.Translate(2 * Time.deltaTime * speed, 0, 0);
    	} else {
    		transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
    	}
    }

    private void Stop()
    {
    	movement = false;
    	animator.SetBool("Movement", movement);
    	action = false;
    	speed = 0;
    }

    private void Hit()
    {
    	Debug.Log("Hit");
    	Invoke("Stop", 5);
    }
}
