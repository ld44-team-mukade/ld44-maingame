using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrator : MonoBehaviour
{
    public static Vibrator instance;

    [SerializeField]
    float _spring = 1f;
    [SerializeField]
    float _damper = 1f;

    [SerializeField]
    float _mass = 1f;

    Vector3 _velocity;
    Vector3 _position;
    // Start is called before the first frame update
    void Awake(){
        if(!instance) instance = this;
    }
    void Start()
    {
        _velocity = Vector3.zero;
        _position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        AddForce(-_position*_spring);
        AddForce(-_velocity*_damper);
        Integrate();
        transform.localPosition = _position;
    }

    public void AddForce(Vector3 force){
        // var localForce = (transform.position - position).normalized*force.magnitude;
        _velocity += force / _mass * Time.deltaTime;
    }

    void Integrate(){
        _position += _velocity*Time.deltaTime;
    }
}
