using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouseController : MonoBehaviour
{

    public float rotate;
    public GameObject lightObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        lightObj.transform.Rotate(new Vector3(0f, rotate, 0f) * Time.deltaTime);

    }
}
