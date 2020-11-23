using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 _movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float x, y, z;
    [SerializeField] float _period = 2f;
    [Range(0, 1)] [SerializeField] float _movementFactor;

    Vector3 _startingPos;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.eulerAngles.x;
        y = transform.eulerAngles.y;
        z = transform.eulerAngles.z;

        Vector3 vectorRotation;
        vectorRotation.x = x;
        vectorRotation.y = y;
        vectorRotation.z = z;

        transform.eulerAngles = vectorRotation;
        _startingPos = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (_period == Mathf.Epsilon)
            return;

        CalculateMovementFactor();
    }

    private void CalculateMovementFactor()
    {
        float cycles = Time.time / _period;
        const float Tau = Mathf.PI * 2;
        float RawSineWave = Mathf.Sin(cycles * Tau);

        _movementFactor = RawSineWave / 2f + 0.5f;
        Vector3 offset = _movementFactor * _movementVector;
        transform.eulerAngles = _startingPos + offset;
    }
}
