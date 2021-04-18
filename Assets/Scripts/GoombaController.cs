using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
	public int speed;
	public bool moveRight = false;
    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      	Movement();
    }

    private void Movement()
    {
    	if (moveRight) {
    		transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
    	} else {
    		transform.Translate(2 * Time.deltaTime * speed, 0, 0);
    	}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    	if (collision.gameObject.CompareTag("Ground")){
    		moveRight = !moveRight;
    	}
    }
}
