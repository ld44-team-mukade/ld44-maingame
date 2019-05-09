using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float _power = 100000000f;

    public ParticleSystem particle;

 /*   private void OnCollisionEnter(Collision collision)
    {
        var bullet = collision.gameObject.GetComponent<BulletAttack>();

        if (bullet != null)
        {
            bullet.BulletDestroy();
            ItemExplosion();

        }

    }
*/
    // Start is called before the first frame update
    void Start()
    {
        ItemExplosion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemExplosion()
    {
        var particle1 = Instantiate(particle, transform.position, transform.rotation) as ParticleSystem;

        Destroy(this.gameObject, particle.main.duration);

        var diffToCameraPos = (Camera.main.transform.position - transform.position);
        var distance = Mathf.Clamp(diffToCameraPos.magnitude, 200f, Mathf.Infinity);
        var direction = diffToCameraPos.normalized;

        Vibrator.instance.AddForce(_power/(distance*distance)*direction);
    }
}
