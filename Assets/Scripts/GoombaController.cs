using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
	public int speed;
	private bool moveRight = false;
    private bool isCrushed;
    public Animator animator;

    void Start()
    {
    	animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      	Movement();
    }

    private void Movement()
    {
    	if (moveRight) {
    		transform.Translate(2 * Time.deltaTime * speed, 0, 0);
    	} else {
    		transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
    	}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    	if (collision.gameObject.CompareTag("Plataformas 1")  || collision.gameObject.CompareTag("Monster"))
    	{
    		moveRight = !moveRight;
    	}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {	
    	float yOffset = 1.9f;

    	if (collision.gameObject.CompareTag("Player") && (transform.position.y + yOffset < collision.transform.position.y))
    	{
    		speed = 0;
    		StartCoroutine(Death());
    	}
    	else if (collision.gameObject.CompareTag("Player"))
    	{
    		Debug.Log("Muere Ame");
    	}
    }


    private IEnumerator Death()
    {
        animator.Play("Death");

        float counter = 0;
        float waitTime = 1.0f;
        
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
