using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip RicardoDamage;
    static AudioSource audioSrc;

    void Start()
    {
        RicardoDamage = Sounds.Load<AudioClip>("RicardoDamage");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
    	switch(clip){
    		case "RicardoDamage":
    			audioSrc.PlayOneShot(RicardoDamage);
    			break;
    	}
    }
}
