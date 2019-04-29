using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{
    public GameObject ship;
    public Engine engine = null;
    public float idol = 30;
    public float max = 123;
    public bool reverse = false;
    // Start is called before the first frame update
    void Start()
    {
        if(engine == null)
        {
            engine = ship.GetComponent<Engine>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float rot = max * engine.currentPower + idol;
        transform.Rotate(0, 0, rot * (reverse?-1f:1f));
    }
}
