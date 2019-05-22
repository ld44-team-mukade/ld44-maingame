using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // 発射するもの
    public BulletAttack bulletPrefab;

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

    [SerializeField]
    private GameObject particle;

    private AkAmbient akAmbient;

    private Vector3 _prevPosition;

    void Awake()
    {
        akAmbient = GetComponent<AkAmbient>();
        _prevPosition = transform.position;
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
    }

    void LateUpdate(){
        _prevPosition = transform.position;
    }

    private void Test()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Fire("");
            //StartParticle();
        }
    }

    public BulletAttack Fire(string tag)
    {
        if(akAmbient != null)
        {
            akAmbient.HandleEvent(null);
        }

        var cannonVelocity = (transform.position - _prevPosition)/Time.deltaTime;

        var bulletInstance = Instantiate(bulletPrefab, _muzzle.position, _muzzle.rotation) as BulletAttack;
        bulletInstance.tag = tag;
        var bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = _muzzle.transform.forward * bulletInstance.initSpeed;// + cannonVelocity;
        bulletInstance.transform.parent = null;
        ChangeLayersRecursively(bulletInstance.transform, tag + "Bullet");
        StartParticle();
        return bulletInstance;
    }

    public void StartParticle()
    {
        GameObject particle1 = Instantiate(particle, _muzzle.transform.position, transform.rotation);
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
    void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }
}
