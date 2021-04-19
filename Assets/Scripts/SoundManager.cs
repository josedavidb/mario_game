using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip RicardoDamage, RicardoDead, RicardoShoot, RicardoWalk, AmeJump, kill, clear, death, roll;
    static AudioSource audioSrc;

    void Start()
    {
        RicardoDamage = Resources.Load<AudioClip>("RicardoDamage");
        RicardoDead = Resources.Load<AudioClip>("RicardoDead");
        RicardoShoot = Resources.Load<AudioClip>("RicardoShoot");
        RicardoWalk = Resources.Load<AudioClip>("RicardoWalk");
        AmeJump = Resources.Load<AudioClip>("smb_jump-small");
        kill = Resources.Load<AudioClip>("kill");
        clear = Resources.Load<AudioClip>("smb_world_clear");
        death = Resources.Load<AudioClip>("smb_mariodie");
        roll = Resources.Load<AudioClip>("smb_bump");
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
            case "jump":
                audioSrc.PlayOneShot(AmeJump);
                break;
            case "kill":
                audioSrc.PlayOneShot(kill);
                break;
            case "clear":
                audioSrc.PlayOneShot(clear);
                break;
            case "death":
                audioSrc.PlayOneShot(death);
                break;
            case "roll":
                audioSrc.PlayOneShot(roll);
                break;
        }
    }
}
