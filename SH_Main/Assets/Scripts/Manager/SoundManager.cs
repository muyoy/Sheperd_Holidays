using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource sfxSource;
    public AudioSource bgmSource;
    int bgmnum = -1;

    public float sfxVolume;
    public float bgmVolume;

    protected override void Initializations()
    {
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
    }

    public void BGMPlayerDB(int _index)
    {
        AudioClip BGM = Resources.Load<AudioClip>(DBManager.instance.BGM_SoundData[_index].path);
        if (!bgmnum.Equals(_index))
        {
            bgmnum = _index;
            BgmPlayer(BGM);
        }
    }

    public void EffectPlayerDB(int _index)
    {
        AudioClip Effect = Resources.Load<AudioClip>(DBManager.instance.SFX_SoundData[_index].path);
        PlayEffectSound(this.gameObject, Effect);
    }

    /// <summary>
    /// 새로운 버전 효과음플레이!
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="obj"></param>
    public void EffectPlayerDB(int _index, GameObject obj)
    {
        PlayEffectSound(obj, Resources.Load<AudioClip>(DBManager.instance.SFX_SoundData[_index].path));
    }

    public void BgmPlayer(AudioClip _clip)
    {
        bgmSource.clip = _clip;
        bgmSource.Play();
        bgmSource.volume = bgmVolume;
        bgmSource.loop = true;
    }

    private void PlayEffectSound(GameObject obj, AudioClip clip)
    {
        AudioSource audioSource;
        if (obj.GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            audioSource = obj.AddComponent<AudioSource>();
        }
        audioSource.volume = sfxVolume;
        audioSource.PlayOneShot(clip);
    }

    public void BgmVolume(float value)//슬라이더가 움직일 때마다 호출되어 사운드의 조절을 해준다.
    {
        bgmVolume = value;
        bgmSource.volume = bgmVolume;
    }
    public void SfxVolume(float value)//슬라이더가 움직일 때마다 호출되어 사운드의 조절을 해준다.
    {
        sfxVolume = value;
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }
}