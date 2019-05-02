using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank: MonoBehaviour
{
    [SerializeField]
    private float _fuelAmount = 100f;
    private float _initialfuelAmount;

    [SerializeField]
    private float _durationTimeToDestroy = 5f;

    [SerializeField]
    private ShipMovement _shipMovement;

    [SerializeField]
    private GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        _initialfuelAmount = _fuelAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecrementFuel(float amount){
        _fuelAmount -= Mathf.Max(0f, amount);
        if(_fuelAmount <= 0f){
            StartParticle();
            Destroy(gameObject, _durationTimeToDestroy);
            _shipMovement.DestroyAgent();
        }
    }
    public void StartParticle()
    {
        GameObject particle1 = Instantiate(particle, transform.position, transform.rotation);
    }

    public void IncrementFuel(float amount){
        _fuelAmount += Mathf.Max(0f, amount);
    }

    public float Remaining(){
        return _fuelAmount;
    }
    public float InitialfuelAmount(){
        return _initialfuelAmount;
    }

}
