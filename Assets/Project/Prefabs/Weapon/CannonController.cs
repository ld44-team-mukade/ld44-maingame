using System.Collections;
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
    public Transform target;
    public Vector3 targetPosition;

    [SerializeField]
    private Transform _turret;

    [SerializeField]
    private Transform _barrel;

    [SerializeField]
    private Transform _muzzle;

    private AkAmbient akAmbient;

    void Awake()
    {
        akAmbient = GetComponent<AkAmbient>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection;
        if(target){
            targetDirection = target.position - _turret.transform.position;
        }else{
            targetDirection = targetPosition - _turret.transform.position;
        }
        RotateTurret(targetDirection);
        // Test();
    }

    private void Test()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Fire("");
        }
    }

    public Rigidbody Fire(string tag)
    {
        if(akAmbient != null)
        {
            akAmbient.HandleEvent(gameObject);
        }
        var bulletInstance = Instantiate(bulletPrefab, _muzzle.transform) as Rigidbody;
        bulletInstance.tag = tag;
        bulletInstance.velocity = _muzzle.transform.forward * initSpeed;
        bulletInstance.transform.parent = null;
        return bulletInstance;
    }

    void RotateTurret(Vector3 targetDirection){
        var localDirection = transform.InverseTransformDirection(targetDirection);
        Vector3 localDirectionOnXZ = localDirection; localDirectionOnXZ.y = 0;
        localDirectionOnXZ.z = Mathf.Max(0, localDirectionOnXZ.z);
        var qY = Quaternion.LookRotation (localDirectionOnXZ);
        _turret.localRotation = qY;
        Vector3 localDirectionOnWY = new Vector3(0, localDirection.y, localDirectionOnXZ.magnitude);
        localDirectionOnWY.y = Mathf.Max(0, localDirectionOnWY.y);
        var qX = Quaternion.LookRotation (localDirectionOnWY);
        _barrel.localRotation = qX;
    }

    public Vector3 BulletCenter(){
        return _turret.position;
    }

    public float Cost(){
        return bulletPrefab.GetComponent<BulletAttack>().cost;
    }
}
