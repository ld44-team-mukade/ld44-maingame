using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipId : MonoBehaviour
{
    [HideInInspector]
    public int Id = -1;
    // Start is called before the first frame update

    private FuelTank _fuelTank;

    void Awake()
    {
        if(!_fuelTank) _fuelTank = gameObject.GetComponent<FuelTank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsLiving(){
        return 0 < _fuelTank.Remaining();
    }
}
