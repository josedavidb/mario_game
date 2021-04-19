using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public float Speed;
    private Vector2 Direction;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
    	Rigidbody2D.velocity = Direction * Speed;
    	if (Direction.x >= 0.0f) 
        {
        	transform.localScale = new Vector3(5f, 5f, 1.0f);
        } else 
        {
        	transform.localScale = new Vector3(-5f, 5f, 1.0f);
        }
    }

    public void SetDirection(Vector2 direction)
    {
    	Direction = direction;
    }

    public void DestroyBullet()
    {
    	Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {	
    	if (collision.gameObject.CompareTag("Player"))
    	{
    		Debug.Log("Muere Ame");
    		DestroyBullet();
    	}
    }
}