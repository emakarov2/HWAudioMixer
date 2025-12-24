using UnityEngine;
using UnityEngine.Audio;

public class BackGroundMusic : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] private AudioMixer _mainMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _backgroundMusic;

    private void Start()
    {
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        if (_musicSource != null && _backgroundMusic != null)
        {
            _musicSource.clip = _backgroundMusic;
            _musicSource.Play();
        }
    }
}