using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SFXManager : MonoBehaviour
{

    public List<AudioSource> radioSounds;
    private List<AudioSource> yawnSounds;
    private bool activated = false;

    public Transform yawnBucket;
    // Start is called before the first frame update
    public void Awake()
    {
        yawnSounds = new List<AudioSource>();
        activated = false;
        foreach (Transform t in yawnBucket)
            yawnSounds.Add(t.gameObject.GetComponent<AudioSource>());
    }
    
    public void StartGame()
    {

        if (activated) return;
        activated = true;
        Debug.Log("STARTING SFX\n");
        StopAllCoroutines();
        StartCoroutine(PlaySFX("radio", 5f));
        StartCoroutine(PlayYawnSFX(5f));
    }
       
    
    private IEnumerator PlayYawnSFX(float interval)
    {
        

        AudioSource currentSound = null;
        int current = 0;
        float waitTime = 0f;       
        while (true)
        {
            current = UnityEngine.Random.Range(0, yawnSounds.Count-1);
            currentSound = yawnSounds[current];
            Debug.Log($"PLAYING YAWNING SFX {currentSound.gameObject.name}\n");
            currentSound.Play();
            while (currentSound.isPlaying)
            {
                yield return new WaitForSecondsRealtime(0.5f);
            }

            current++;
            if (current == radioSounds.Count) current = 0;
            yield return new WaitForSecondsRealtime(interval);
        }

    }

    private IEnumerator PlaySFX(string name, float interval)
    {
        

        AudioSource currentSound = null;
        int current = 0;
        float waitTime = 0f;
        while (true)
        {
            currentSound = radioSounds[current];
            if (currentSound == null) continue;           
            currentSound.Play();
            while (currentSound.isPlaying)
            {
                yield return new WaitForSecondsRealtime(0.5f);
            }

            current++;
            if (current == radioSounds.Count) current = 0;
            yield return new WaitForSecondsRealtime(interval);
        }

    }
    
  
    
    public void PlaySound(string name)
    {

        AudioSource sound = null;
        for(int i = 0; i < radioSounds.Count; i++)
            if (radioSounds[i].name.Equals(name))
                sound = radioSounds[i];


        if (sound == null) return;
        
        sound.Play();
    }
}
