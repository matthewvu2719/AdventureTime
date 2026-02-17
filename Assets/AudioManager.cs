using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private int bgmIndex;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;


    private void Update()
    {
        if (!bgm[bgmIndex].isPlaying)
        {
            bgm[bgmIndex].Play();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    public void PlaySFX(int sfxIndex)
    {
        if(sfxIndex < sfx.Length)
        {
            sfx[sfxIndex].pitch =Random.Range(0.85f,1.15f);
            sfx[sfxIndex].Play();
        }
    }

    public void StopSFX(int sfxIndex)
    {
        sfx[sfxIndex].Stop();
    }

    public void StopBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }


    public void PlayBGM(int bgIndex)
    {
        bgmIndex= bgIndex;
        StopBGM();
        bgm[bgIndex].Play();
    }

    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }
}
