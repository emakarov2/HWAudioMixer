using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteToggleHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer _mainMixer;
    [SerializeField] private Button _button;
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private Slider _volumeSlider;

    private bool _isMuted = false;
    private float _savedVolume;

    private void OnEnable()
    {
        if (_button != null)
        {
            _button.onClick.AddListener(ToggleMute);
        }
    }

    private void OnDisable()
    {
        if (_button != null)
        {
            _button.onClick.RemoveListener(ToggleMute);
        }
    }

    public void Unmute()
    {
        _isMuted = true;
        ToggleMute();
    }

    private void ToggleMute()
    {
        int notMagicalTwenty = 20;
        float silence = -80f;

        _isMuted = _isMuted == false;

        if (_mainMixer != null && _mixerGroup != null)
        {
            if (_isMuted)
            {
                if (_volumeSlider != null)
                {
                    _savedVolume = _volumeSlider.value;
                }

                _mainMixer.SetFloat(_mixerGroup.name, silence);
            }
            else
            {
                float volumeDB = Mathf.Log10(_savedVolume) * notMagicalTwenty;
                _mainMixer.SetFloat(_mixerGroup.name, volumeDB);
            }
        }
    }
}