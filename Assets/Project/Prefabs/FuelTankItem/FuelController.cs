using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelController : MonoBehaviour
{
    public float fuelCapacity;

    private void OnCollisionEnter(Collision collision)
    {
        var ship = collision.gameObject.GetComponent<FuelTank>();
        if(!ship)return;
        if (collision.gameObject.tag == "Player")
        {
            ship.IncrementFuel(fuelCapacity);

            Destroy(this.gameObject);
        }
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
