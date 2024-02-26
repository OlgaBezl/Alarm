using System;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class AlarmArea : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Thief _))
        {
            _alarm.On();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out Thief _))
        {
            _alarm.MuteDown();
        }
    }
}
