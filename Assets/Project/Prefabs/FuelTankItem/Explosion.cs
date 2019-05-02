using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public ParticleSystem particle;
    public float deltime;

    private void OnCollisionEnter(Collision collision)
    {
        var bullet = collision.gameObject.GetComponent<BulletAttack>();

        if (bullet != null)
        {
            bullet.BulletDestroy();
            ItemExplosion();
            
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

    public void ItemExplosion()
    {
        Destroy(this.gameObject);
        var particle1 = Instantiate(particle, transform.position, transform.rotation) as ParticleSystem;

    }
}
