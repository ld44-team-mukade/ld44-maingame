using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTankItemAnimation : MonoBehaviour
{

    //アイテムの移動範囲
    public int range;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(speed * Mathf.PI * Time.time);

        transform.position = new Vector3(transform.position.x, sin * range, transform.position.z);
    }

}
