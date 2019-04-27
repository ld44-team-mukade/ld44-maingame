using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipTargetAgent : MonoBehaviour
{
    [SerializeField]
    public Vector3 target = Vector3.zero;
    NavMeshAgent _agent;
    // Start is called before the first frame update

    void Awake(){
        _agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // _agent.Move(_target);
        _agent.SetDestination(target);
    }
}
