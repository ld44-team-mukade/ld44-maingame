using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    //unityから直接値を変更できるように
    public float fuelparam;

    //相手のShipにあたった時にShipの「DecrementFuel」を呼び出す
    private void OnCollisionEnter(Collision collision)
    {

        var ship = collision.gameObject.GetComponent<FuelTank>();

        if (ship != null)
        {
            ship.DecrementFuel(fuelparam);
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
