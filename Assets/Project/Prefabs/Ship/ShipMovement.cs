using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _targetHeight = 0f;

    [SerializeField]
    private NavMeshAgent _targetAgentPrefab;

    [SerializeField]
    private Vector3 _agentTargetPosition;

    private NavMeshAgent _targetAgentInstance;
    
    
    [SerializeField]
    private float verticalMovementPower = 10f;

    [SerializeField]
    private float verticalMovementDamper = 1f;

    void Awake(){
        _rigidbody = GetComponent<Rigidbody>();
        _targetAgentInstance = Instantiate(_targetAgentPrefab, transform.position, transform.rotation) as NavMeshAgent;
        _targetAgentInstance.transform.parent = null;
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
        _targetAgentInstance.SetDestination(_agentTargetPosition);
    }

    void FixedUpdate(){
        // var fixedTarget = _targetAgentInstance.transform.position;
        // fixedTarget.y = transform.position.y;
        // _rigidbody.MovePosition(fixedTarget);
        // var heightDiff = transform.position.y - _targetAgentInstance.transform.position.y;
        // _rigidbody.AddForce(-Vector3.up * heightDiff*Time.fixedDeltaTime * verticalMovementPower);
        // _rigidbody.AddForce(-Vector3.up * _rigidbody.velocity.y*Time.fixedDeltaTime * verticalMovementDamper);

        var diff = transform.position - _targetAgentInstance.transform.position;
        _rigidbody.AddForce(-diff*Time.fixedDeltaTime * verticalMovementPower);
        _rigidbody.AddForce(-_rigidbody.velocity * Time.fixedDeltaTime * verticalMovementDamper);

        var toAgent = (_targetAgentInstance.transform.position - transform.position);
        if(20.0f < _rigidbody.velocity.magnitude){
            var torque = Vector3.Cross(transform.forward, toAgent.normalized);
            Debug.DrawLine(transform.position,transform.position + _rigidbody.velocity.normalized*100f, Color.blue);
            Debug.DrawLine(transform.position,transform.position + torque*100f);
            Debug.Log(torque);
            _rigidbody.AddTorque(0, torque.y*Time.fixedDeltaTime*100000f, 0);
            _rigidbody.AddTorque(-_rigidbody.angularVelocity*Time.fixedDeltaTime*10000f);
            // MoveTo(transform.position + Vector3.forward*Time.fixedDeltaTime*10f);
        }
    }
}
