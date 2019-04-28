using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelExplosion : MonoBehaviour
{

    public GameObject particle;
    public float deltime;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
 
            Destroy(this.gameObject);
            GameObject particle1 = Instantiate(particle, transform.position, transform.rotation);

            Destroy(particle1, deltime);

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
