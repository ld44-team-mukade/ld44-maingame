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
    void Start()
    {

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
