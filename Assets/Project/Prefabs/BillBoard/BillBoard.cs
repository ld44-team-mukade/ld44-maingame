using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    public float size;
    Vector3 objScale;

    // Start is called before the first frame update
    void Start()
    {
        objScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 objPos = this.transform.position;

        // cameraPos.y = transform.position.y;
        transform.LookAt(cameraPos);

        float dis = Vector3.Distance(cameraPos, objPos);

        // dis = dis / size;

        transform.localScale = objScale + (Vector3.one * dis * size);
    }


}
