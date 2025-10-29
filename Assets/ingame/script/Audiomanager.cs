using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;

    [Header("#BGM")]
    public AudioClip bgmclip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxclips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Crickets, Fire = 6, Turn, UI, Coin, Step, Catch = 16, Error}

    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        //배경음
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmclip;

        //효과음
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if(sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            int ranIndex = 0;
            if(sfx == Sfx.Crickets || sfx == Sfx.Step)
            {
                ranIndex = Random.Range(0, 6);
            }

            channelIndex = loopIndex;
            sfxPlayers[0].clip = sfxclips[(int)sfx + ranIndex];
            sfxPlayers[0].Play();
            break;
        }
    }
}
