using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // 発射するもののサイズ
    public Vector3 bulletSize = new Vector3(1, 1, 1);
    // 発射するもの
    public Rigidbody bullet;
    // 銃身になるオブジェクト
    public GameObject barrel;
    // 旋回軸
    public GameObject pivot;
    // 上下軸
    public GameObject pitch;
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
    }

    private void Test()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Fire();
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            pivot.transform.Rotate(0, 1f, 0);
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            pivot.transform.Rotate(0, -1f, 0);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            pitch.transform.Rotate(-1f, 0, 0);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            pitch.transform.Rotate(1f, 0, 0);
        }
    }

    public Rigidbody Fire()
    {
        Vector3 offset = barrel.transform.up;
        offset  *= barrel.transform.lossyScale[1] + this.bullet.transform.lossyScale[1];

        Rigidbody bullet = Instantiate(this.bullet, barrel.transform.position + offset, barrel.transform.rotation);
        bullet.transform.localScale = this.bulletSize;
        Vector3 velocity = barrel.transform.up * initSpeed;
        bullet.velocity = velocity;
        return bullet;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
