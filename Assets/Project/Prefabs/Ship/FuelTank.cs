using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank: MonoBehaviour
{
    [SerializeField]
    private float _fuelAmount = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecrementFuel(float amount){
        _fuelAmount -= amount;
    }
    public void IncrementFuel(float amount){
        _fuelAmount += amount;
    }

    public float Remaining(){
        return _fuelAmount;
    }

}
