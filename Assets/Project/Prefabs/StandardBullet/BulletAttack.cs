using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    //unityから直接値を変更できるように
    public float fuelparam;
    public float lifeTime = 4f;
    public float cost = 1f;

    [SerializeField]
    private GameObject particle;

    [SerializeField]
    private Collider collider;

    public int age = 0;
    private void OnCollisionEnter(Collision collision)
    {
        //相手のShipにあたった時にShipの「DecrementFuel」を呼び出す
        TryOnCollisionEnterShip(collision);

        //砲弾の削除
        BulletDestroy();
        StartParticle(collision);
    }

    private void TryOnCollisionEnterShip(Collision collision){
        var ship = collision.gameObject.GetComponent<FuelTank>();
        if (ship == null) return;
        if (ship.tag == tag) return;

        //Shipの燃料減らす
        ship.DecrementFuel(fuelparam);
    }


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    // Update is called once per frame
    void Update()
    {
        if(5 < age) collider.enabled = true;
        age++;
    }
    public void StartParticle(Collision col)
    {
        GameObject particle1 = Instantiate(particle, col.transform.position, transform.rotation);
    }
    public void BulletDestroy()
    {
        Destroy(this.gameObject);
    }
}
