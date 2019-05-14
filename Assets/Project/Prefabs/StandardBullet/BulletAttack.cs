using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{

    public float fuelparam = 50;

    public float lifeTime = 4f;

    public float cost = 1f;

    public float initSpeed = 100f;

    [SerializeField]
    private GameObject particle;

    [SerializeField]
    private Collider collider;

    [SerializeField]
    private float _blastRadius = 5;

    public int age = 0;

    [SerializeField]
    Rigidbody _rigidbody;

    [SerializeField]
    float _maxDamage = 40f;

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

        BlastDamage(transform.position);

        //Shipの燃料減らす
        ship.DecrementFuel(fuelparam);   
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        _rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(5 < age) collider.enabled = true;
        age++;
    }


    //爆風のダメージ計算
    public void BlastDamage(Vector3 blastCenter)
    {
        Collider[] colliders = Physics.OverlapSphere(blastCenter, _blastRadius);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == null) return;

            GameObject colliderParent = colliders[i].attachedRigidbody.gameObject;
            var ship = colliderParent.GetComponent<FuelTank>();

 //           if (colliderParent == null) return;
            if (ship != null)
            {

                Vector3 playerPos = colliders[i].ClosestPoint(blastCenter);
                float distance = Vector3.Distance(blastCenter, playerPos);
                
                float blastDamage = (_blastRadius - distance)/_blastRadius*_maxDamage;
                
                ship.DecrementFuel(blastDamage);

                if(DebugFlag.IsDebug()){
                    Debug.Log("爆風ダメージ　＝　" + blastDamage + colliderParent.name);
                }
            }else if (ship == null) return;

        }
        
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
