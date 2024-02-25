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

    private void Update()
    {
        if (_sound.isPlaying)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _targetVolume, _recoveryRate * Time.deltaTime);
            
            if (_sound.volume <= _minVolume)
            {
                _sound.Stop();
            }
        }
    }

    private void On()
    {
        _targetVolume = _maxVolume;
        _sound.Play();
    }

    private void MuteDown()
    {
        _targetVolume = _minVolume;
    }    
}
