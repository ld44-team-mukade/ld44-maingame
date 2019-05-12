using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelController : MonoBehaviour
{
    public float fuelCapacity;
    
    [SerializeField]
    private float delTime;

    //[SerializeField]
    //private GameObject explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        TryOnCollisionEnterPlayer(collision);
        //TryOnCollisionEnterBullet(collision);
    }

    private void TryOnCollisionEnterPlayer(Collision collision)
    {
        var ship = collision.gameObject.GetComponent<FuelTank>();
        if (!ship) return;
        if (collision.gameObject.tag == "Player")
        {
            ship.IncrementFuel(fuelCapacity);
            
            Destroy(this.gameObject);
        }
    }
/*
    private void TryOnCollisionEnterBullet(Collision collision)
    {
        var bullet = collision.gameObject.GetComponent<BulletAttack>();
        if(!bullet)return;
        Destroy(this.gameObject);
        var explosionInstance = Instantiate(explosionPrefab, transform);
    }
 */
    // Start is called before the first frame update
        void Start()
    {
        Destroy(this.gameObject, delTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
