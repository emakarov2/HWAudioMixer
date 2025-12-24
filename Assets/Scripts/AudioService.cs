using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioService : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] private AudioMixer _mainMixer;

    [Header("Sliders")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("Buttons")]
    [SerializeField] private Button _muteButton;
    [SerializeField] private Button _soundAK47Button;
    [SerializeField] private Button _soundConnectButton;
    [SerializeField] private Button _soundRunButton;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _soundAK47;
    [SerializeField] private AudioClip _soundConnect;
    [SerializeField] private AudioClip _soundRun;

    private bool _isMuted = false;
    private float _savedMasterVolume;

    private string _masterVolume = "MasterVolume";
    private string _musicVolume = "MusicVolume";
    private string _SFXVolume = "SFXVolume";

    void Start()
    {
        SetupSliders();
        SetupButtons();
        PlayBackgroundMusic();
    }

    void SetupSliders()
    {
        _masterSlider.onValueChanged.AddListener(OnMasterChanged);
        _musicSlider.onValueChanged.AddListener(OnMusicChanged);
        _sfxSlider.onValueChanged.AddListener(OnSFXChanged);

        OnMasterChanged(_masterSlider.value);
        OnMusicChanged(_musicSlider.value);
        OnSFXChanged(_sfxSlider.value);
        SetMixerVolume("SFXVolume", _sfxSlider.value);
    }

    void SetupButtons()
    {
        _muteButton.onClick.AddListener(() =>
        {
            ToggleMute();
        });

        _soundAK47Button.onClick.AddListener(() =>
        {
            PlaySound(_soundAK47);
        });

        _soundConnectButton.onClick.AddListener(() =>
        {
            PlaySound(_soundConnect);
        });

        _soundRunButton.onClick.AddListener(() =>
        {
            PlaySound(_soundRun);
        });
    }

    void PlayBackgroundMusic()
    {
        if (_musicSource != null && _backgroundMusic != null)
        {
            _musicSource.clip = _backgroundMusic;
            _musicSource.Play();
        }
    }

    void OnMasterChanged(float value)
    {
        _savedMasterVolume = value;
        if (!_isMuted)
        {
            SetMixerVolume(_masterVolume, value);
        }
    }

    void OnMusicChanged(float value)
    {
        SetMixerVolume(_musicVolume, value);
    }

    void OnSFXChanged(float value)
    {
        SetMixerVolume(_SFXVolume, value);
    }

    void SetMixerVolume(string parameter, float value)
    {
        int notMagicalTwenty = 20;
        
        float volumeDB = Mathf.Log10(value) * notMagicalTwenty;
        _mainMixer.SetFloat(parameter, volumeDB);
    }

    void ToggleMute()
    {
        float silence = -80f;

        _isMuted = _isMuted == false;

        if (_isMuted)
        {
            _mainMixer.SetFloat(_masterVolume, silence);
        }
        else
        {
            SetMixerVolume(_masterVolume, _savedMasterVolume);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (_sfxSource != null && clip != null)
        {
            _sfxSource.PlayOneShot(clip);
        }
    }
}