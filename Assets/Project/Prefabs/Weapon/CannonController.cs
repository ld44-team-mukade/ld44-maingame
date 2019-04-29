﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // 発射するもの
    public Rigidbody bulletPrefab;

    // 弾の初速
    public float initSpeed = 100f;

    // 狙う相手(まだ何も使ってない)
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _turret;

    [SerializeField]
    private Transform _barrel;

    [SerializeField]
    private Transform _muzzle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var targetDirection = _target.position - _turret.transform.position;
        RotateTurret(targetDirection);
        Test();
    }

    private void Test()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Fire();
        }
    }

    public Rigidbody Fire()
    {
        var bulletInstance = Instantiate(bulletPrefab, _muzzle.transform) as Rigidbody;
        bulletInstance.velocity = _muzzle.transform.forward * initSpeed;
        bulletInstance.transform.parent = null;
        return bulletInstance;
    }

    void RotateTurret(Vector3 targetDirection){
        var localDirection = transform.InverseTransformDirection(targetDirection);
        Vector3 localDirectionOnXZ = localDirection; localDirectionOnXZ.y = 0;
        var qY = Quaternion.LookRotation (localDirectionOnXZ);
        _turret.localRotation = qY;
        Vector3 localDirectionOnWY = new Vector3(0, localDirection.y, localDirectionOnXZ.magnitude);
        var qX = Quaternion.LookRotation (localDirectionOnWY);
        _barrel.localRotation = qX;
    }

    public Vector3 BulletCenter(){
        return _turret.position;
    }
}
