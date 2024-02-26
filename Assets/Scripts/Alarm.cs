using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AlarmArea _area;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _recoveryRate = 0.5f;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _targetVolume;
    private Coroutine _soundCoroutine;

    private void Start()
    {
        _sound.volume = _minVolume; 
    }

    private void OnEnable()
    {
        _area.ThiefEntered += On;
        _area.ThiefExited += MuteDown;
    }

    private void OnDisable()
    {
        _area.ThiefEntered -= On;
        _area.ThiefExited -= MuteDown;
    }

    private IEnumerator PlaySound()
    {
        var wait = new WaitForFixedUpdate();

        while (_sound.isPlaying)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _targetVolume, _recoveryRate * Time.deltaTime);

            if (_sound.volume <= _minVolume)
            {
                StopCoroutine(_soundCoroutine);
                _sound.Stop();
            }

            yield return wait;
        }
    }

    private void On()
    {
        _targetVolume = _maxVolume;

        if (_sound.isPlaying == false)
        {
            _sound.Play();
            _soundCoroutine = StartCoroutine(PlaySound());
        }
    }

    private void MuteDown()
    {
        _targetVolume = _minVolume;
    }    
}
