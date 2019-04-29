using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CameraMove(Transform target,float _rotate)
    {

        float mouseInputX = Input.GetAxis("Mouse X");
        float mouseInputY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(1))
        {
            //transform.RotateAround(target.transform.position, Vector3.up, _rotate * mouseInputX);
            transform.RotateAround(target.position, transform.right, _rotate * mouseInputY);

        }

    }

}
