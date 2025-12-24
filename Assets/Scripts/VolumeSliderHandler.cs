using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderVolumeHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer _mainMixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private MuteToggleHandler _muteToggleHandler;
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private void OnEnable()
    {
        if (_slider != null)
        {
            _slider.onValueChanged.AddListener(OnSliderChanged);
        }
    }

    private void Start()
    {
        OnSliderChanged(_slider.value);
    }

    private void OnDisable()
    {
        if (_slider != null)
        {
            _slider.onValueChanged.RemoveListener(OnSliderChanged);
        }
    }

    private void OnSliderChanged(float value)
    {
        int notMagicalTwenty = 20;
        float silence = -80f;

        _muteToggleHandler.Unmute();

        if (value == 0)
        {
            _mainMixer.SetFloat(_mixerGroup.name, silence);
        }
        else if (_mainMixer != null && _mixerGroup != null)
        {
            float volumeDB = Mathf.Log10(value) * notMagicalTwenty;
            _mainMixer.SetFloat(_mixerGroup.name, volumeDB);
        }
    }
}