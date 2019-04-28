using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    //unityから直接値を変更できるように
    public float fuelparam;
    GameObject fuel;

    //相手のShipにあたった時にShipの「DecrementFuel」を呼び出す
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ship")
        {
            //
            fuel = GameObject.Find("Ship");
            fuel.GetComponent<FuelTank>().DecrementFuel(fuelparam);
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
