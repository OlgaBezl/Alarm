using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _recoveryRate = 0.5f;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _targetVolume;
    private Coroutine _coroutine;

    private void Start()
    {
        _sound.volume = _minVolume; 
    }

    private IEnumerator ChangeVolume()
    {
        var wait = new WaitForFixedUpdate();

        while (_sound.isPlaying)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _targetVolume, _recoveryRate);

            if (_sound.volume <= _minVolume)
            {
                StopCoroutine(_coroutine);
                _sound.Stop();
            }

            yield return wait;
        }
    }

    public void On()
    {
        _targetVolume = _maxVolume;

        if (_sound.isPlaying == false)
        {
            _sound.Play();
            _coroutine = StartCoroutine(ChangeVolume());
        }
    }

    public void MuteDown()
    {
        _targetVolume = _minVolume;
    }    
}
