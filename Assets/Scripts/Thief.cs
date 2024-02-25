using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";

    [SerializeField] private float _speed = 5f;

    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis(HorizontalAxis), 0).normalized;
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }
}
