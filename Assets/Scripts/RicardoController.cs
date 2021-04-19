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

    public void Action()
    {
    	action = true;
    	System.Random random = new System.Random();
    	int num = random.Next(1,4);
    	if (num == 1) Shoot();
    	else if (num == 2) Hit();
    	else if (num == 3) Walk();
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
    	Debug.Log("Start Movement");
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
    	Debug.Log("Stop");
    }

    private void Hit()
    {
    	Debug.Log("Hit");
    	Invoke("Stop", 5);
    }
}
