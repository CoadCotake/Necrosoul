using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManage_BGM : MonoBehaviour
{
    // Make it a singleton class that can be accessible everywhere
    public static AudioManage_BGM instance;
    public AudioMixerGroup BGM_mixer;
    public IEnumerator fadein;
    public IEnumerator fadeout;
    
    [SerializeField]
    Sound[] m_sounds;

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogError("More than one AudioManger in scene");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DonDestoryManage.DDM.DDM_List.Add(this.gameObject);
        for (int i = 0; i < m_sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + m_sounds[i].m_name);
            go.transform.SetParent(transform);
            m_sounds[i].SetSource(go.AddComponent<AudioSource>());
            m_sounds[i].m_source.outputAudioMixerGroup = BGM_mixer;
        }

    }

    public void PlaySound (string name)
    {
        for(int i = 0; i < m_sounds.Length; i++)
        {
            if(m_sounds[i].m_name == name)
            {
                Debug.Log("사운드 플레이 중");
                StartCoroutine(StartAudio_fadein(m_sounds[i]));
                return;
            }
        }

        Debug.LogWarning("AudioManager: Sound name not found in list: " + name);
    }

    public void StopSound(string name)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].m_name == name && m_sounds[i].IsPlaying())
            {
                StartCoroutine(StartAudio_fadeout(m_sounds[i]));
                return;
            }
        }
    }

    public bool IsPlaying(string name)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].m_name == name && m_sounds[i].IsPlaying())
            {
                return true;
            }
        }

        return false;
    }


    IEnumerator StartAudio_fadein(Sound BGM)
    {
        Debug.Log("코루틴 실행"+BGM.m_name);
        BGM.Play();
        BGM.m_source.volume = 0;
        while (BGM.m_source.volume < BGM.Original_volume)
        {
            for (int i = 0; i <= 100; i++)
            {
                BGM.m_source.volume += Time.deltaTime * 0.005f;
            }
            yield return null;
        }
    }
    IEnumerator StartAudio_fadeout(Sound BGM)
    {
        while (BGM.Original_volume > 0)
        {
            for (int i = 100; i >= 0; i--)
            {
                BGM.m_source.volume -= Time.deltaTime * 0.005f;
            }
            yield return null;
        }
        BGM.Stop();
    }

    // 배경음악

    public void Main_Select_BGM()
    {
        PlaySound("Main_Select_BGM");
    }
    public void Main_Select_BGM_e()
    {
        m_sounds[0].Play();
    }

    public void Stage1()
    {
        StopSound("Main_Select_BGM");
        PlaySound("Stage1");
    }








}
