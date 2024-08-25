using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SounManager : MonoBehaviour
{

    public AudioClip bombExplosion, fallingSwoosh, bombBroken,looseSound,winSound;
    public AudioSource bombSource;
    public AudioSource winSource;

    IEnumerator Loose()
    {
        yield return new WaitForSeconds(1f);
        winSource.PlayOneShot(looseSound,0.75f);
    }

    public void Explode()
    {
        bombSource.PlayOneShot(bombExplosion, 0.75f);
    }
    public void Broken()
    {
        bombSource.PlayOneShot(bombBroken, 0.75f);
    }
    public void Fall()
    {
        bombSource.PlayOneShot(fallingSwoosh, 0.75f);
    }

    public void Victory()
    {
        winSource.PlayOneShot(winSound, 0.75f);
    }

    public void Lose()
    {
        StartCoroutine(Loose());
    }

}
