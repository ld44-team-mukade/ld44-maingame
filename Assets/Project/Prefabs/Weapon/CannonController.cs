using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // 射出するもの
    public Rigidbody bullet;
    // 銃身になるオブジェクト
    public GameObject barrel;
    // 弾の初速
    public int initSpeed = 100;
    // 狙う相手(まだ何も使ってない)
    protected Transform target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Fire();
        }
    }

    public Rigidbody Fire()
    {
        Vector3 offset = transform.up;
        // Y軸に沿って射出する
        // 軸を変えるなら書き換えて
        offset  *= barrel.transform.localScale.y + this.bullet.transform.localScale.y;

        Rigidbody bullet = Instantiate(this.bullet, barrel.transform.position + offset, barrel.transform.rotation);
        Vector3 velocity = barrel.transform.up * initSpeed;
        bullet.velocity = velocity;
        return bullet;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
