using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    static public MusicManager singleton;

    [Header("Музыка")]
    [SerializeField] private ClipListSO clipList;
    [SerializeField, Range(0f, 1f)] private float normalVolume;

    [Header("Snapshots")]
    [SerializeField] private AudioMixerSnapshot normal;
    [SerializeField] private AudioMixerSnapshot pause;
    [SerializeField] private AudioMixerSnapshot menu;

    [Header("Данные доп")]
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private MusicStatSO musicStats;
    [SerializeField] private AudioSource audioSource;

    [Header("UI data")]
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private Font font;
    [SerializeField] private Color textColor;
    [SerializeField] private Color textAlternativeColor;

    [Header("UI elements")]
    [SerializeField] private GameObject volumeOptions;
    [SerializeField] private Image backgrounfImage;
    [SerializeField] private List<Text> allTexts;
    [SerializeField] private Slider masterVolSlider;
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider effectsVolSlider;

    #region Awake Start
    private void Awake()
    {
        ToSnapshot(normal, 1f);
        Singleton();
        singleton = this;
    }

    void Singleton()
    {
        MusicManager[] musics = FindObjectsOfType<MusicManager>();
        for (int i = 0; i < musics.Length; i++)
        {
            if (musics[i].gameObject != gameObject)
            {
                Destroy(gameObject);
                break;
            }
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(musicStats)
            LoadMusicOptions();

        clipList.Shuffle();
        NextMusic();
    }
    #endregion

    #region NextClip
    public void NextMusic() => NextMusic(clipList.NextClip(), normalVolume);

    public void NextMusic(AudioClip clip, float volume = 1f)
    {
        audioSource.clip = clip;
        audioSource.volume = Mathf.Lerp(0f, 1f, volume);
        audioSource.Play();
        StartCoroutine(NextAudioClip(clip));
    }

    IEnumerator NextAudioClip(AudioClip clip)
    {
        float time = clip.length;
        yield return new WaitForSeconds(time);
        if (audioSource.clip.name == clip.name)
        {
            NextMusic();
        }
    }
    #endregion

    #region Изменение настроек звука
    void LoadMusicOptions()
    {
        // установка параметров slider
        LoadToUI(masterVolSlider, musicStats.master.volume, MixerGroup.MasterVolume);
        LoadToUI(musicVolSlider, musicStats.music.volume, MixerGroup.MusicVolume);
        LoadToUI(effectsVolSlider, musicStats.effects.volume, MixerGroup.EffectsVolume);
    }

    #region Sliders
    /// <summary>
    /// установка значения на сам UI element и выстановление параметров в mixerGroup
    /// </summary>
    void LoadToUI(Slider slider, float value, string mixerGroup)
    {
        slider.value = value;    // кнопка самого toggle
        SetFloatMixer(mixerGroup, value);
    }

    /// <summary>
    /// Сохраниение нового значения и установка его в mixerGroup
    /// </summary>
    void NewValue(string mixerGroup, ref float oldValue, float newValue)
    {
        SetFloatMixer(mixerGroup, newValue);
        oldValue = newValue;
    }

    /// <summary>
    /// Вкл/выкл звука нужного mixerGroup
    /// </summary>
    void SetFloatMixer(string mixerGroup, float value) => mixer.audioMixer.SetFloat(mixerGroup, SaveFloatToVolume(value));

    float SaveFloatToVolume(float value) => Mathf.Lerp(-80, 0, value);
    #endregion

    #region UIActions
    public void VolumeMaster(float value) => NewValue(MixerGroup.MasterVolume, ref musicStats.master.volume, value);
    public void VolumeMusic(float value) => NewValue(MixerGroup.MusicVolume, ref musicStats.music.volume, value);
    public void VolumeEffects(float value) => NewValue(MixerGroup.EffectsVolume, ref musicStats.effects.volume, value);
    #endregion

    #region Выбор Snapshots
    public void ToMenuSnapshot() => ToSnapshot(menu);
    public void ToNormalSnapshot() => ToSnapshot(normal);
    public void ToPauseSnapshot() => ToSnapshot(pause);
    public void ToSnapshot(AudioMixerSnapshot snapshot, float timeTransition = 1f)
    {
        if (!snapshot) return;

        snapshot.TransitionTo(timeTransition);
    }
    #endregion

    #endregion
}

public static class MixerGroup
{
    public static string MusicVolume = "Volume Music";
    public static string MasterVolume = "Volume Master";
    public static string EffectsVolume = "Volume Effects";
}
