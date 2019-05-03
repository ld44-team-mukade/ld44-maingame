using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShipController : MonoBehaviour
{
    [SerializeField]
    private ShipMovement _shipMovement;

    [HideInInspector]
    public GameSpace gameSpace;
    // Start is called before the first frame update

    [SerializeField]
    private FCS _fcs;

    [SerializeField]
    private float _shootableRange = 100f;

    [SerializeField]
    private float _shotRate = 0.5f;

    void Start()
    {
        StartCoroutine(TryShotCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        // if(_shipMovement.PathStatus() != NavMeshPathStatus.PathComplete){
        //     SetTargetPositionRandomly();
        // }
        if(!_shipMovement.IsMovingWithAgent(50f)){
            Debug.Log("Search Path");
            SetTargetPositionRandomly();
        }

    }

    IEnumerator TryShotCoroutine(){
        while(true){
            TryShot();
            yield return new WaitForSeconds(_shotRate);
        }
    }

    void TryShot(){
        if(!_fcs.CurrentTarget()) return;
        var distance = Vector3.Distance(_fcs.CurrentTarget().transform.position, transform.position);
        if(_shootableRange < distance) return;

        _fcs.Fire();
    }

    void SetTargetPositionRandomly(){
        bool isReachable = false;
        do{
            var heightIndex = Random.Range(0, gameSpace.heights.Count);
            var direction = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up) * Vector3.forward;
            var distance = Random.Range(0f, gameSpace.radius);
            var height = gameSpace.heights[heightIndex];
            var candidatePosition = direction * distance;
            candidatePosition.y = height;
            isReachable = _shipMovement.ChangeTargetAgentPosition(candidatePosition);
        }while(!isReachable);
    }
}
