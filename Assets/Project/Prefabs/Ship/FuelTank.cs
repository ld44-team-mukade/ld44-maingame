using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank: MonoBehaviour
{
    [SerializeField]
    private float _fuelAmount = 100f;

    [SerializeField]
    private float _durationTimeToDestroy = 5f;

    [SerializeField]
    private ShipMovement _shipMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecrementFuel(float amount){
        _fuelAmount -= Mathf.Max(0f, amount);
        if(_fuelAmount <= 0f){
            Destroy(gameObject, _durationTimeToDestroy);
            _shipMovement.DestroyAgent();
        }
    }
    public void IncrementFuel(float amount){
        _fuelAmount += Mathf.Max(0f, amount);
    }

    public float Remaining(){
        return _fuelAmount;
    }

}
