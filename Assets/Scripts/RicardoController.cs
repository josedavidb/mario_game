using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class RicardoController : MonoBehaviour
{
    public GameObject Ame;
    public GameObject Bullet;
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
        	Invoke("Action", 2);
        	LastAction = Time.time;
        }
        if (movement) Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {	
    	if (collision.gameObject.CompareTag("Attack") && Health == 1 && !action)
    	{
    		action = true;
    		speed = 0;
    		Health -= 1;
            this.tag = "Untagged";
            transform.Find("RicardoCollider").tag = "Untagged";
            StartCoroutine(Death());
    	}
    	if (collision.gameObject.CompareTag("Attack") && !action){
    		StartCoroutine(Damage());
    		Health -= 1;
    	}
    }

    private IEnumerator Damage()
    {
    	animator.Play("RicardoDamage");
    	SoundManager.PlaySound("RicardoDamage");
    	float counter = 0;
        float waitTime = animator.GetCurrentAnimatorStateInfo(0).length;

        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        animator.Play("RicardoNormal");
    }

    private IEnumerator Death()
    {
    	animator.Play("RicardoDeath");
    	SoundManager.PlaySound("RicardoDead");
    	float counter = 0;
    	float waitTime = 2.0f;

        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

    	Destroy(gameObject);
    }

    public void Action()
    {
    	action = true;
    	System.Random random = new System.Random();
    	int num = random.Next(1,3);
    	if (num == 1) Walk();
    	else if (num == 2) Shoot();
    	else Stop();
    	//else if (num == 3) Hit();
    }

    private void Shoot()
    {
    	if (Health != 0){
    		SoundManager.PlaySound("RicardoShoot");
    		animator.SetBool("Shoot", true);
 			Invoke("BulletShoot", 1);
    	}
    	Invoke("Stop", 2);
    }

    private void BulletShoot()
    {
    	Vector3 direction;
    	if (transform.localScale.x == 2.5f) direction = Vector3.right;
    	else direction = Vector3.left;

    	GameObject bullet = Instantiate(Bullet, transform.position + direction * 0.1f, Quaternion.identity);
    	bullet.GetComponent<BulletController>().SetDirection(direction);
    }

    private void Walk()
    {
    	if (Health != 0) StartMovement();
    	Invoke("Stop", 2);
    }

    private void StartMovement()
    {
    	SoundManager.PlaySound("RicardoWalk");
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
    	animator.SetBool("Shoot", false);
    	action = false;
    	speed = 0;
    }

    private void Hit()
    {
    	Debug.Log("Hit");
    	Invoke("Stop", 5);
    }
}
