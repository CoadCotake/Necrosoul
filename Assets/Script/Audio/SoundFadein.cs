using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFadein : MonoBehaviour
{
    public AudioSource BackGround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayingSound()
    {
        BackGround.volume = 0;
        BackGround.Play();
        StartCoroutine("StartAudio_fadein");
    }

    IEnumerator StartAudio_fadein()
    {
        while (BackGround.volume < 1)
        {
            for (int i = 0; i <= 100; i++)
            {
                BackGround.volume += Time.deltaTime * 0.005f;
            }
            yield return null;
        }
    }
}
