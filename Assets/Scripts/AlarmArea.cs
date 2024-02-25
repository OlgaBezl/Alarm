using System;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class AlarmArea : MonoBehaviour
{
    public event Action ThiefEntered;
    public event Action ThiefExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Thief thief))
        {
            ThiefEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out Thief thief))
        {
            ThiefExited?.Invoke();
        }
    }
}
