using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigController : MonoBehaviour
{
    public Transform target;
    //回転カメラ用
    public GameObject rcamera;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float distance;

    [SerializeField]
    private float _smoothTime = 0.5f;

    [SerializeField]
    private float _rotatespeed;

    [SerializeField]
    private float scrollSpeed;

    [SerializeField]
    private float scrollMin;

    [SerializeField]
    private float scrollMax;

    private Vector3 _velocity;

    void Awake(){
        _velocity = Vector3.zero;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (!target) return;
        var direction = _camera.transform.rotation * Vector3.forward;
        // _camera.transform.position = target.position - direction * distance;
        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, target.position - direction * distance, ref _velocity, _smoothTime);
        // _camera.transform.position = target.position - direction * distance;

        rcamera.GetComponent<CameraRotate>().CameraMove(target,_rotatespeed);

        distance -= scroll * scrollSpeed;

        if (distance > scrollMax)
        {
            distance = scrollMax;
        }
        else if (distance < scrollMin)
        {
            distance = scrollMin;
        }

    }
}
