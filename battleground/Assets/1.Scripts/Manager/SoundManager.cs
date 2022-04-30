using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingletonMonobehaviour<SoundManager>
{
    public const string MasterGroupName = "Master";
    public const string EffectGroupName = "Effect";
    public const string BGMGroupName = "BGM";
    public const string UIGroupName = "UI";
    public const string MixerName = "AudioMixer";
    public const string ContainerName = "SoundContainer";
    public const string FadeA = "FadeA";
    public const string FadeB = "FadeB";
    public const string UI = "UI";
    public const string EffectVolumeParam = "Volume_Effect";
    public const string BGMVolumeParam = "Volume_BGM";
    public const string UIVolumeParam = "Volume_UI";

    public enum MusicPlayingType
    {
        None = 0,
        SourceA = 1, // 페이드 관련
        SourceB = 2,
        AtoB = 3, // 배경음 관련
        BtoA = 4,
    }

    public AudioMixer mixer = null;
    public Transform audioRoot = null;
    public AudioSource fadeA_audio = null;
    public AudioSource fadeB_audio = null;
    public AudioSource[] effect_audios = null; // 많은 이펙트 사운드가 동시에 재생되면 오디오가 찢어지기에 이펙트 사운드 채널 개수를 제한을 두어 사전에 방지한다. 
    public AudioSource UI_audio = null;

    public float[] effect_PlayStartTime = null; //더이상 이펙트 사운드 채널이 추가될 공간이 없다면 가장 오래된 사운드를 끄고 new sound 재생 
    private int EffectChannelCount = 5;
    private MusicPlayingType currentPlayingType = MusicPlayingType.None;
    private bool isTricking = false;
    private SoundClip currentSound = null;
    private SoundClip lastClip = null;
    private float minVolume = -80.0f;
    private float maxVolume = 0.0f;


    void Start()
    {
        if(this.mixer == null)
        {
            this.mixer = Resources.Load(MixerName) as AudioMixer;
        }
        if( this.audioRoot == null)
        {
            audioRoot = new GameObject(ContainerName).transform;
            audioRoot.SetParent(transform);
            audioRoot.localPosition = Vector3.zero;
        }
        if(fadeA_audio != null)
        {
            GameObject fadeA = new GameObject(FadeA, typeof(AudioSource));
            fadeA.transform.SetParent(audioRoot);
            this.fadeA_audio = fadeA.GetComponent<AudioSource>(); // 사운드 재생.
            this.fadeA_audio.playOnAwake = false;
        }
        if(fadeB_audio == null)
        {
            GameObject fadeB = new GameObject(FadeB, typeof(AudioSource));
            fadeB.transform.SetParent(audioRoot);
            fadeB_audio = fadeB.GetComponent<AudioSource>();
            fadeB_audio.playOnAwake = false;
        }
        if(UI_audio == null)
        {
            GameObject ui = new GameObject(UI, typeof(AudioSource));
            ui.transform.SetParent(audioRoot);
            UI_audio = ui.GetComponent<AudioSource>();
            UI_audio.playOnAwake = false;
        }
        if(this.effect_audios == null && this.effect_audios.Length == 0)
        {
            this.effect_PlayStartTime = new float[EffectChannelCount];
            this.effect_audios = new AudioSource[EffectChannelCount];
            for(int i = 0; i < EffectChannelCount; i++)
            {
                effect_PlayStartTime[i] = 0.0f;
                GameObject effect = new GameObject("Effect" + i.ToString(), typeof(AudioSource));
                effect.transform.SetParent(audioRoot);
                this.effect_audios[i] = effect.GetComponent<AudioSource>();
                this.effect_audios[i].playOnAwake = false;
            }
        }
        if (mixer != null)
        {
            this.fadeA_audio.outputAudioMixerGroup = mixer.FindMatchingGroups(BGMVolumeParam)[0];
            this.fadeB_audio.outputAudioMixerGroup = mixer.FindMatchingGroups(BGMVolumeParam)[0];// 오디오 볼륨 조절.
            this.UI_audio.outputAudioMixerGroup = mixer.FindMatchingGroups(UIVolumeParam)[0];
            for (int i = 0; i < this.effect_audios.Length; i++) // 이펙트
            {
                this.effect_audios[i].outputAudioMixerGroup = mixer.FindMatchingGroups(EffectGroupName)[0];
            }
        }

    }

    public void SetBGMVolume(float currentRatio)//현재비율
    {
        currentRatio = Mathf.Clamp01(currentRatio); // 0과 1 사이에 값을 정해줌.
        float voluem = Mathf.Lerp(minVolume, maxVolume, currentRatio);
        this.mixer.SetFloat(BGMVolumeParam, voluem);
        PlayerPrefs.SetFloat(BGMVolumeParam, voluem);
    }

    public float GetBGMVolume()
    {
        if (PlayerPrefs.HasKey(BGMVolumeParam))
        {
            return Mathf.Lerp(minVolume, maxVolume, PlayerPrefs.GetFloat(BGMVolumeParam));
        }
        else
        {
            return maxVolume;
        }
    }

    void Update()
    {
        
    }


}
