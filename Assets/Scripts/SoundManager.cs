using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip RicardoDamage, RicardoDead, RicardoShoot, RicardoWalk;
    static AudioSource audioSrc;

    void Start()
    {
        RicardoDamage = Resources.Load<AudioClip>("RicardoDamage");
        RicardoDead = Resources.Load<AudioClip>("RicardoDead");
        RicardoShoot = Resources.Load<AudioClip>("RicardoShoot");
        RicardoWalk = Resources.Load<AudioClip>("RicardoWalk");

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
    		case "RicardoDead":
    			audioSrc.PlayOneShot(RicardoDead);
    			break;
    		case "RicardoShoot":
    			audioSrc.PlayOneShot(RicardoShoot);
    			break;
    		case "RicardoWalk":
    			audioSrc.PlayOneShot(RicardoWalk);
    			break;	
    	}
    }
}
