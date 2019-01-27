using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySound{
        public string myName;
        public AudioSource audioSource;
    }
    

public class SFXManager : MonoBehaviour
{

    public List<MySound> sounds;
    // Start is called before the first frame update
    public void StartGame()
    {
        StartCoroutine(PlaySFX());
    }

    private IEnumerator PlaySFX()
    {
        yield return new WaitForSecondsRealtime(3f);
        
            
        
        yield return null;
    }


    public void PlaySound(string name)
    {

        MySound sound = null;
        for(int i = 0; i < sounds.Count; i++)
            if (sounds[i].myName.Equals(name))
                sound = sounds[i];


        if (sound == null) return;
        
        sound.audioSource.Play();
    }
}
