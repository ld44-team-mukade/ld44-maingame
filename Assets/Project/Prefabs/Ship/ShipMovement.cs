using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipMovement : MonoBehaviour
{
    public bool isManualControll = false;

    public Vector3 manualForce= Vector3.zero;

    [SerializeField]
    private FuelTank _fuelTank;

    private Rigidbody _rigidbody;

    [SerializeField]
    private float _targetHeight = 0f;

    [SerializeField]
    private float _maxDifference = 30.0f;

    [SerializeField]
    private NavMeshAgent _targetAgentPrefab;

    [SerializeField]
    private Vector3 _agentTargetPosition;

    private NavMeshAgent _targetAgentInstance;
    
    [SerializeField]
    private float verticalMovementPower = 10f;

    [SerializeField]
    private float verticalMovementDamper = 1f;
    private Vector3 _latestAgentPosition;

    [SerializeField]
    private float _floatingConsumption = 0f;
    void Awake(){
        _rigidbody = GetComponent<Rigidbody>();
        _targetAgentInstance = Instantiate(_targetAgentPrefab, transform.position, transform.rotation) as NavMeshAgent;
        _targetAgentInstance.transform.parent = null;
        _latestAgentPosition = _targetAgentInstance.transform.position;
        // _targetAgentInstance.transform.parent = transform.
    }
    public void MoveTo(Vector3 target){
        // _agentTargetPosition = target;
        target.y = transform.position.y;
        _rigidbody.MovePosition(target);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _fuelTank.DecrementFuel((1f+_fuelTank.Remaining()/1000f)*_floatingConsumption * Time.deltaTime);
        // ChangeTargetAgentPosition(_agentTargetPosition);
    }

    void FixedUpdate(){
        if(0f < _fuelTank.Remaining()){
            Move();
        }
    }

    void Move(){
        if(isManualControll){
            _rigidbody.AddForce(manualForce* Time.fixedDeltaTime * verticalMovementPower);
            _rigidbody.AddForce(-_rigidbody.velocity * Time.fixedDeltaTime * verticalMovementDamper);

        }else{
            if(!_targetAgentInstance.isOnNavMesh){
                NavMeshHit hit;
                bool isHitting = _targetAgentInstance.FindClosestEdge(out hit);
                Debug.Assert(isHitting, "Set NavMesh.");
                ChangeCurrentAgentPosition(hit.position);
            }
            var target = _targetAgentInstance.transform.position;
            var diff = transform.position - target;
            diff = Vector3.ClampMagnitude(diff, _maxDifference);
            _rigidbody.AddForce(-diff * Time.fixedDeltaTime * verticalMovementPower);
            _rigidbody.AddForce(-_rigidbody.velocity * Time.fixedDeltaTime * verticalMovementDamper);
        }

        var velocityOnPlane = _rigidbody.velocity;
        velocityOnPlane.y = 0f;

        var fixedVelocity = _rigidbody.velocity; fixedVelocity.y = 0f;
        Vector3 yaw = Vector3.Project(Vector3.Cross(fixedVelocity.normalized, transform.forward), Vector3.up);
        var yawForce = -yaw * 100000f;
        _rigidbody.AddTorque(Clamp(yawForce, 0, 20000f));

        // Vector3 pitch = Vector3.ProjectOnPlane(Vector3.Cross(fixedVelocity*0.1f, transform.forward), transform.right);
        // Vector3 pitch = Vector3.ProjectOnPlane(Vector3.Cross(fixedVelocity*0.1f, transform.forward), transform.right);
        Vector3 pitch = -Vector3.Project(Vector3.Cross(Vector3.up, transform.up), transform.right) - transform.right*_rigidbody.velocity.y*0.002f;
        var pitchForce = pitch* 100000f;
        _rigidbody.AddTorque(Clamp(pitchForce, 0, 20000f));


        if(1.0f < velocityOnPlane.magnitude){
            // Quaternion targetRot = Quaternion.LookRotation(fixedVelocity, Vector3.up);
            // float y = Vector3.Project(fixedVelocity.normalized, transform.up);
            // Quaternion targetRot = Quaternion.FromToRotation(transform.forward, fixedVelocity);
            // _rigidbody.AddTorque(targetRot.eulerAngles*10f);
            // _rigidbody.AddTorque(-targetRot.eulerAngles*10f);
            // var q = Quaternion.RotateTowards(_rigidbody.rotation, targetRot, 50f * Time.fixedDeltaTime);
            // _rigidbody.MoveRotation(q);
        }
        Vector3 rollBaseVector = Vector3.ProjectOnPlane(Vector3.up, transform.forward).normalized;
        Vector3 roll = Vector3.Project(Vector3.Cross(rollBaseVector, transform.up), transform.forward);
        var rollForce = -roll* 100000f;
        _rigidbody.AddTorque(Clamp(rollForce, 0, 20000f));

        _rigidbody.AddTorque(-_rigidbody.angularVelocity * 20000f);
    }

    Vector3 Clamp(Vector3 vec, float min, float max){
        var dir = vec.normalized;
        var mag = Mathf.Clamp(vec.magnitude, min, max);
        return dir * mag;
    }
    public void DestroyAgent(){
        Destroy(_targetAgentInstance.gameObject);
    }

    public void ChangeCurrentAgentPosition(Vector3 position){
        _targetAgentInstance.Warp(position);
    }

    public Vector3 CurrentAgentPosition(){
        return _targetAgentInstance.transform.position;
    }

    public bool ChangeTargetAgentPosition(Vector3 position){
        _targetAgentInstance.Resume();
        return _targetAgentInstance.SetDestination(position);
    }

    public bool IsReachable(Vector3 position){
        NavMeshPath path = new NavMeshPath();
        return _targetAgentInstance.CalculatePath(transform.position, path);
    }

    public NavMeshPathStatus PathStatus(){
        return _targetAgentInstance.pathStatus;
    }

    public bool IsMovingWithAgent(float distance){
        if(!_targetAgentInstance)return false;
        return _targetAgentInstance.remainingDistance > distance;
    }
}
