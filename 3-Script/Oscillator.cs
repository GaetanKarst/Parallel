using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 _movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    [Range(0,1)] float _movementFactor;//todo Remove from the inspector later

    private Vector3 _startingPos;

    // Start is called before the first frame update
    void Start()
    {
        _startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }    
        CalculatMovementFactor();
    }

    private void CalculatMovementFactor()
    {
        float cycles = Time.time / period;//automatically framerate independant
        const float Tau = Mathf.PI * 2; //about 6.28 1tau = 2PI
        float rawSinWave = Mathf.Sin(cycles * Tau);//goes from -1 to +1

        _movementFactor = rawSinWave / 2f + 0.5f; //goes to 0 to 1
        Vector3 offset = _movementFactor * _movementVector;
        transform.position = _startingPos + offset;
    }
}
