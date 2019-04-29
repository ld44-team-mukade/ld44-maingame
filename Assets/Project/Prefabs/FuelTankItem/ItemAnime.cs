using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnime : MonoBehaviour
{

    //アイテムの移動範囲
    public int range;
    public int speed;
    float sin1 = 0;
    float sin2 = 0;

    // Start is called before the first frame update
    void Start()
    {
 

    }

    // Update is called once per frame
    void Update()
    {
        sin1 = Mathf.Sin(speed * Mathf.PI * Time.time);        

        transform.position = new Vector3(transform.position.x, transform.position.y+(sin1 * range) - sin2, transform.position.z);

        sin2 = sin1 * range;
    }

}
