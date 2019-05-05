using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    private float _maxIntensity;
    private float _currentTime = 1f;
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
        float dayNightRatio = (Mathf.Sin(_currentTime)*0.5f+0.5f);
        _light.intensity = dayNightRatio * _maxIntensity;
        _currentTime += _speed*Time.deltaTime;
    }
}
