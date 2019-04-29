using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    //unityから直接値を変更できるように
    public float fuelparam;
    public float lifeTime = 4f;
    public float cost = 1f;

    //相手のShipにあたった時にShipの「DecrementFuel」を呼び出す
    private void OnCollisionEnter(Collision collision)
    {
        var ship = collision.gameObject.GetComponent<FuelTank>();
        if (ship == null) return;
        if (ship.tag == tag) return;

        //Shipの燃料減らす
        ship.DecrementFuel(fuelparam);
        //砲弾の削除
        BulletDestroy();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void BulletDestroy()
    {
        Destroy(this.gameObject);
    }
}
