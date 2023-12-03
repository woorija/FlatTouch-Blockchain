using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Audio
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : SingletonBehaviour<AudioManager>
{
    [SerializeField] Audio[] bgm;
    [SerializeField] Audio[] sfx;

    [SerializeField] AudioSource bgmPlayer;
    [SerializeField] AudioSource[] sfxPlayer;

    float volume;
    public string currentBgmName { get; private set; } = string.Empty;
    Coroutine MainBGMPlayingCheckCoroutine;
    public void VolumeControl()
    {
        volume=GameManager.Instance.GetVolume(); ;
        bgmPlayer.volume = volume;
        foreach (AudioSource sfx in sfxPlayer)
        {
            sfx.volume = volume;
        }
    }
    public void PlayMainBGM()
    {
        if (!currentBgmName.Equals("MainBGM"))
        {
            MainBGMPlayingCheckCoroutine = StartCoroutine(PlayMainBGMCoroutine());
        }
    }
    public void StopMainBGM()
    {
        StopBgm();
        if (MainBGMPlayingCheckCoroutine != null)
        {
            StopCoroutine(MainBGMPlayingCheckCoroutine);
            MainBGMPlayingCheckCoroutine = null;
        }
    }
    IEnumerator PlayMainBGMCoroutine()
    {
        PlayBgm("MainBGM");
        while (true)
        {
            yield return new WaitForSecondsRealtime(GetBGMLength() + 3f);
            PlayBgm("MainBGM");
        }
    }
    public void PlayBgm(string _name)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (_name == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                currentBgmName = bgm[i].name;
                bgmPlayer.Play();
                return;
            }
        }
    }
    public void StopBgm()
    {
        if (bgmPlayer.clip != null)
        {
            bgmPlayer.Stop();
            currentBgmName = string.Empty;
        }
    }

    public void PauseBgm()
    {
        if (bgmPlayer.clip != null)
        {
            bgmPlayer.Pause();
        }
    }

    public void UnpauseBgm()
    {
        if (bgmPlayer.clip != null)
        {
            bgmPlayer.UnPause();
        }
    }
    public float GetBGMLength()
    {
        if (bgmPlayer.clip != null)
        {
            return bgmPlayer.clip.length;
        }
        return 0;
    }
    public void PlaySfx(string _name)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (_name == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                return;
            }
        }
    }
}
