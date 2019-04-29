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
        // ChangeTargetAgentPosition(_agentTargetPosition);
    }

    void FixedUpdate(){
        if(0f < _fuelTank.Remaining()){
            Move();
        }
    }

    void Move(){
        Vector3 target = Vector3.zero;
        if(isManualControll){
            _rigidbody.AddForce(manualForce* Time.fixedDeltaTime * verticalMovementPower);
            _rigidbody.AddForce(-_rigidbody.velocity * Time.fixedDeltaTime * verticalMovementDamper);

        }else{
            target = _targetAgentInstance.transform.position;
            // transform.position = _targetAgentInstance.transform.position;
            var diff = transform.position - target;
            diff = Vector3.ClampMagnitude(diff, _maxDifference);
            _rigidbody.AddForce(-diff * Time.fixedDeltaTime * verticalMovementPower);
            _rigidbody.AddForce(-_rigidbody.velocity * Time.fixedDeltaTime * verticalMovementDamper);

            // var toAgent = (_targetAgentInstance.transform.position - transform.position);
            // if(20.0f < _rigidbody.velocity.magnitude){
            // var torque = Vector3.Cross(transform.forward, toAgent.normalized);
            // Debug.DrawLine(transform.position,transform.position + _rigidbody.velocity.normalized*100f, Color.blue);
            // Debug.DrawLine(transform.position,transform.position + torque*100f);
            // _rigidbody.AddTorque(0, torque.y*Time.fixedDeltaTime*100000f, 0);
            // _rigidbody.AddTorque(-_rigidbody.angularVelocity*Time.fixedDeltaTime*10000f);
            // }
        }

        var velocityOnPlane = _rigidbody.velocity;
        velocityOnPlane.y = velocityOnPlane.y * 0.5f;
        Quaternion targetRot = Quaternion.LookRotation(velocityOnPlane);
        var q = Quaternion.RotateTowards(_rigidbody.rotation, targetRot, 50f * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(q);
    }

    public void DestroyAgent(){
        Destroy(_targetAgentInstance);
    }

    public void ChangeCurrentAgentPosition(Vector3 position){
        // _targetAgentInstance.transform.position = position;
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
        return _targetAgentInstance.remainingDistance > distance;
    }
}
