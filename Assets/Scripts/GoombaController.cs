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
        if ((collision.gameObject.transform.name.IndexOf("RollHitbox") >= 0) || (collision.gameObject.transform.name.IndexOf("FloorHitBox") >= 0 && transform.position.y < collision.transform.position.y))
        {
            this.tag = "Untagged";
            transform.Find("GoombaCollider").tag = "Untagged";
            isCrushed = true;
            animator.SetBool("IsCrushed", isCrushed);
            speed = 0;
            StartCoroutine(Death());
        }
    }


    private IEnumerator Death()
    {
        animator.Play("Death");
        SoundManager.PlaySound("kill");
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
