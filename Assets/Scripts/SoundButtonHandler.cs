using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _SFXSource;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        if (_button != null)
        {
            _button.onClick.AddListener(PlaySound);
        }
    }

    private void PlaySound()
    {
        if (_SFXSource != null && _clip != null)
        {
            _SFXSource.PlayOneShot(_clip);
        }
    }

    private void OnDestroy()
    {
        if (_button != null)
        {
            _button.onClick.RemoveListener(PlaySound);
        }
    }
}