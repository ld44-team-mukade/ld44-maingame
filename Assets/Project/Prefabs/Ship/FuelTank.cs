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

    [SerializeField]
    private GameObject _itemGetUI;

    private bool _shouldExlode = false;

    private Rigidbody _rigidbody;

    void Awake(){
        _shouldExlode = false;
        _rigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _initialfuelAmount = _fuelAmount;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate(){
        _rigidbody.mass = 1f + Remaining()/1000f;
    }

    public void DecrementFuel(float amount){
        _fuelAmount = Mathf.Max(0f, _fuelAmount - amount);
        if(_fuelAmount <= 0f && !_shouldExlode){
            var duration = StartParticle()*0.5f;
            Destroy(gameObject, duration);
            _shipMovement.DestroyAgent();
            _shouldExlode = true;
        }
    }
    public float StartParticle()
    {
        GameObject particle1 = Instantiate(particle, transform.position, transform.rotation);
        return particle1.GetComponentInChildren<ParticleSystem>().main.duration;
    }

    public void IncrementFuel(float amount){
        _fuelAmount = Mathf.Max(0f, _fuelAmount + amount);
        var itemGetInfo =_itemGetUI.GetComponent<ItemGetInfo>();
        if (itemGetInfo == null) return;
        itemGetInfo.ItemGetAnime(amount);
        Debug.Log("ItemGetInfo =" + amount);

    }

    public float Remaining(){
        return _fuelAmount;
    }
    public float InitialfuelAmount(){
        return _initialfuelAmount;
    }

}
