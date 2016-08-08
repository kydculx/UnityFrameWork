using UnityEngine;
using System.Collections;
using System;

class FWSoundData
{
    public AudioClip[] m_AudioClip;
    public AudioSource[] m_AudioSource;
    public string[] m_strFileName;

    public FWSoundData()
    {
        m_AudioClip = null;
        m_AudioSource = null;
        m_strFileName = null;
    }

    ~FWSoundData()
    {
        m_AudioClip = null;
        m_AudioSource = null;
        m_strFileName = null;
    }
}

public class FWSoundManager : FWSingleton<FWSoundManager>
{
    FWSoundData m_kEFFData = null;
    FWSoundData m_kBGMData = null;

    bool m_bEFFOnOff;
    bool m_bBGMOnOff;
    float m_fEFFVolume;
    float m_fBGMVolume;

    void Awake()
    {
        m_kEFFData = new FWSoundData();
        m_kBGMData = new FWSoundData();

        m_fEFFVolume = 1.0f;
        m_fBGMVolume = 1.0f;

        m_bEFFOnOff = true;
        m_bBGMOnOff = true;

        PreloadAllEFF("Sound/EFF");
        PreloadAllBGM("Sound/BGM");
    }

    void Destory()
    {
        m_kEFFData = null;
        m_kBGMData = null;
    }

    public void SetEFFOnOff(bool bOnOff)
    {
        m_bEFFOnOff = bOnOff;
    }

    public void SetBGMOnOff(bool bOnOff)
    {
        m_bBGMOnOff = bOnOff;
    }

    bool GetEFFOnOFF()
    {
        return m_bEFFOnOff;
    }

    bool GetBGMOnOff()
    {
        return m_bBGMOnOff;
    }

    int PreloadAllEFF(string strDirectory)
    {
        m_kEFFData.m_AudioClip = Resources.LoadAll<AudioClip>(strDirectory);

        int a_nLength = m_kEFFData.m_AudioClip.Length;

        if (a_nLength >= 1)
        {
            m_kEFFData.m_strFileName = new string[a_nLength];
            m_kEFFData.m_AudioSource = new AudioSource[a_nLength];

            for (int i = 0; i < a_nLength; i++)
            {
                m_kEFFData.m_strFileName[i] = m_kEFFData.m_AudioClip[i].name;
                m_kEFFData.m_AudioSource[i] = gameObject.AddComponent<AudioSource>();
                m_kEFFData.m_AudioSource[i].clip = m_kEFFData.m_AudioClip[i];
                m_kEFFData.m_AudioSource[i].volume = m_fEFFVolume;
            }
        }

        return a_nLength;
    }

    int PreloadAllBGM(string strDirectory)
    {
        m_kBGMData.m_AudioClip = Resources.LoadAll<AudioClip>(strDirectory);

        int a_nLength = m_kBGMData.m_AudioClip.Length;

        if (a_nLength >= 1)
        {
            m_kBGMData.m_strFileName = new string[a_nLength];
            m_kBGMData.m_AudioSource = new AudioSource[a_nLength];

            for (int i = 0; i < a_nLength; i++)
            {
                m_kBGMData.m_strFileName[i]         = m_kBGMData.m_AudioClip[i].name;
                m_kBGMData.m_AudioSource[i]         = gameObject.AddComponent<AudioSource>();
                m_kBGMData.m_AudioSource[i].clip    = m_kBGMData.m_AudioClip[i];
                m_kBGMData.m_AudioSource[i].volume  = m_fBGMVolume;
            }
        }

        return a_nLength;
    }

    public void PlayEFF(string strFileName, bool bLoop = false)
    {
        if (GetEFFOnOFF() == false)
            return;

        AudioClip a_kAudioClip = GetEFFClip(strFileName);
        AudioSource a_kAudioSource = GetEFFSource(strFileName);
        a_kAudioSource.bypassEffects = true;
        a_kAudioSource.loop = bLoop;

        a_kAudioSource.PlayOneShot(a_kAudioClip);
    }

    public void PauseEFF()
    {
        for (int i = 0; i < m_kEFFData.m_AudioSource.Length; i++)
        {
            if (m_kEFFData.m_AudioSource[i].isPlaying)
                m_kEFFData.m_AudioSource[i].Pause();
        }
    }

    public void ResumeEFF()
    {
        if (GetBGMOnOff() == false)
            return;

        for (int i = 0; i < m_kEFFData.m_AudioSource.Length; i++)
        {
            m_kEFFData.m_AudioSource[i].UnPause();
        }
    }

    public void PlayBGM(string strFileName, bool bLoop = false)
    {
        StopAllBGM();

        if (GetBGMOnOff() == false)
            return;

        AudioSource a_kAudioSource = GetBGMSource(strFileName);
        
        a_kAudioSource.bypassEffects = true;
        a_kAudioSource.loop = bLoop;
        a_kAudioSource.Play();
    }

    public void PauseBGM()
    {
        for (int i = 0; i < m_kBGMData.m_AudioSource.Length; i++)
        {
            if (m_kBGMData.m_AudioSource[i].isPlaying)
                m_kBGMData.m_AudioSource[i].Pause();
        }
    }

    public void ResumeBGM()
    {
        if (GetBGMOnOff() == false)
            return;

        for (int i = 0; i < m_kBGMData.m_AudioSource.Length; i++)
        {
            m_kBGMData.m_AudioSource[i].UnPause();
        }
    }

    public void SetEFFVolume(float fVolume)
    {
        m_fEFFVolume = fVolume;

        for (int i = 0; i < m_kEFFData.m_AudioSource.Length; i++)
        {
            m_kEFFData.m_AudioSource[i].volume = m_fEFFVolume;
        }
    }

    public float GetEFFVolume()
    {
        return m_fEFFVolume;
    }

    public void SetBGMVolume(float fVolume)
    {
        m_fBGMVolume = fVolume;

        for (int i = 0; i < m_kBGMData.m_AudioSource.Length; i++)
        {
            m_kBGMData.m_AudioSource[i].volume = m_fBGMVolume;
        }
    }

    public float GetBGMVolume()
    {
        return m_fBGMVolume;
    }

    public void StopAllEFF()
    {
        for (int i = 0; i < m_kEFFData.m_AudioSource.Length; i++)
        {
            if (m_kEFFData.m_AudioSource[i].isPlaying)
                m_kEFFData.m_AudioSource[i].Stop();
        }
    }

    public void StopEFF(string strFileName)
    {
        int a_nIndex = System.Array.IndexOf(m_kEFFData.m_strFileName, strFileName);
        if (m_kEFFData.m_AudioSource[a_nIndex].isPlaying)
            m_kEFFData.m_AudioSource[a_nIndex].Stop();
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < m_kBGMData.m_AudioSource.Length; i++)
        {
            if (m_kBGMData.m_AudioSource[i].isPlaying)
                m_kBGMData.m_AudioSource[i].Stop();
        }
    }

    public void StopGBM(string strFileName)
    {
        int a_nIndex = System.Array.IndexOf(m_kEFFData.m_strFileName, strFileName);
        if (m_kBGMData.m_AudioSource[a_nIndex].isPlaying)
            m_kBGMData.m_AudioSource[a_nIndex].Stop();
    }

    AudioSource GetEFFSource(string strFileName)
    {
        AudioSource a_kAudioSource = null;
        a_kAudioSource = m_kEFFData.m_AudioSource[System.Array.IndexOf(m_kEFFData.m_strFileName, strFileName)];
        Debug.Assert(a_kAudioSource);

        return a_kAudioSource;
    }

    AudioSource GetBGMSource(string strFileName)
    {
        AudioSource a_kAudioSource = null;
        a_kAudioSource = m_kBGMData.m_AudioSource[System.Array.IndexOf(m_kBGMData.m_strFileName, strFileName)];
        Debug.Assert(a_kAudioSource);

        return a_kAudioSource;
    }

    AudioClip GetEFFClip(string strFileName)
    {
        AudioClip kAudioClip = null;
        kAudioClip = m_kEFFData.m_AudioClip[System.Array.IndexOf(m_kEFFData.m_strFileName, strFileName)];
        Debug.Assert(kAudioClip);

        return kAudioClip;
    }

    AudioClip GetBGMClip(string strFileName)
    {
        AudioClip a_kAudioClip = null;
        a_kAudioClip = m_kBGMData.m_AudioClip[System.Array.IndexOf(m_kBGMData.m_strFileName, strFileName)];
        Debug.Assert(a_kAudioClip);

        return a_kAudioClip;
    }
}