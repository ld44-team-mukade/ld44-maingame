using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    private Gradient color;

    [SerializeField]
    private AnimationCurve intensity;

    [SerializeField, Range(0,1)]
    private float _currentTime = 1f;

    [SerializeField]
    private float _maxIntensity;

    [SerializeField]
    private float _speed = 1f;

    private Light _light;
    // Start is called before the first frame update
    void Awake(){
        _light = GetComponent<Light>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _light.color = color.Evaluate((_currentTime)%1.0f);
        _light.intensity = intensity.Evaluate((_currentTime)%1.0f) * _maxIntensity;
        // Debug.Log(_light.);
        _currentTime = (_currentTime + _speed*Time.deltaTime)%1.0f;
    }
}
