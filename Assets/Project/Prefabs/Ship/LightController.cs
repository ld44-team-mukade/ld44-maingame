using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightController : MonoBehaviour
{
    List<Light> _lights;
    FuelTank _fuelTank;

    void Awake(){
        _fuelTank = GetComponent<FuelTank>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _lights = GetComponentsInChildren<Light>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if(0f < _fuelTank.Remaining()) return;
        foreach (var light in _lights)
        {
            light.enabled = false;
        }
    }
}
