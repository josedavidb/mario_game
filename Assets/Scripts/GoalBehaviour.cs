using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoalBehaviour : MonoBehaviour
{
    private bool cleared;
    // Start is called before the first frame update
    void Start()
    {
        cleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameObject.Find("RicardoNormal_0") == null && !cleared)
        {
            StartCoroutine(Outro());
        }
    }

    public IEnumerator Outro()
    {
        cleared = true;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        SoundManager.PlaySound("clear");

        float counter = 0;
        float waitTime = 6.0f; 

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("Credits");
    }
}
