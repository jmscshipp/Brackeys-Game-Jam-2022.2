using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudio : MonoBehaviour
{
    public AudioClip charactersPlacedAudio;
    public AudioClip pressPlayAudio;

    AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCharacterPlacedAudio()
    {
        src.PlayOneShot(charactersPlacedAudio);
    }

    public void PlayLevelStartAudio()
    {
        src.PlayOneShot(pressPlayAudio);
    }
}
