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
    private MusicPlayingType currentPlayingType = MusicPlayingType.None;
    private bool isTricking = false;
    private SoundClip currentSound = null;
    private SoundClip lastClip = null;
    private float minVolume = -80.0f;
    private float maxVolume = 0.0f;


    void Start()
    {
        
    }

    void Update()
    {
        
    }


}
