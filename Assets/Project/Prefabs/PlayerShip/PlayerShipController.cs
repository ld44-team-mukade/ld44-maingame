﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField]
    ShipMovement _shipMovement;
    [SerializeField]
    int currentHeightIndex = 0;

    [SerializeField]
    public GameSpace gameSpace;

    [SerializeField]
    private Transform _cursorPrefab;
    private Transform _cursorInstance;

    [SerializeField]
    private FCS _fcs;

    void Awake(){
        _cursorInstance = Instantiate(_cursorPrefab);
        _shipMovement.isManualControll = true;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _fcs.Fire();
        }

        int direction = 0;
        if(Input.GetKeyDown(KeyCode.Q)){direction += 1;};
        if(Input.GetKeyDown(KeyCode.E)){direction -= 1;};

        var targetHeightIndex = Mathf.Clamp(currentHeightIndex+direction, 0, gameSpace.heights.Count-1);
        if(targetHeightIndex != currentHeightIndex){
            var isSuccessed = TryToChangeHeight(targetHeightIndex);
            if(isSuccessed){
                currentHeightIndex = targetHeightIndex;
            }
            Debug.Log(currentHeightIndex);
        }

        float planeHeight = transform.position.y;
        Vector2 upOnScreen = new Vector2(Screen.width*0.5f,0);
        Vector2 rightOnScreen = new Vector2(Screen.width,Screen.height*0.5f);
        var upOnWorld = ScreenSpaceToPlaneSpace(upOnScreen, planeHeight);
        var rightOnWorld = ScreenSpaceToPlaneSpace(rightOnScreen, planeHeight);

        Vector3 input = -(upOnWorld - transform.position).normalized*Input.GetAxis("Vertical") + (rightOnWorld - transform.position).normalized*Input.GetAxis("Horizontal");
        
        var targetPosition = input*50f;
        targetPosition.y = gameSpace.heights[currentHeightIndex] - transform.position.y;
        _shipMovement.manualForce = targetPosition;


    }

    bool TryToChangeHeight(int index){
        var currentAgentPosition = _shipMovement.CurrentAgentPosition();
        var targetPosition = currentAgentPosition; targetPosition.y = gameSpace.heights[index];
        var direction = (targetPosition - currentAgentPosition).normalized;

        var ray = new Ray(currentAgentPosition, Vector3.up * direction.y);
        RaycastHit hit;
        bool changeable = true;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
            changeable = hit.collider.tag != "Obstacle";
        }
        
        if(!changeable)return false;
        _shipMovement.ChangeCurrentAgentPosition(targetPosition);
        return true;
    }

    Vector3 CursorPosition(float planeHeight){
        Vector2 cursorScreenPosition = Input.mousePosition;
        return ScreenSpaceToPlaneSpace(cursorScreenPosition, planeHeight);
    }

    Vector3 ScreenSpaceToPlaneSpace(Vector2 screenPosition, float planeHeight){
        Vector2 cursorScreenPosition = screenPosition;
        Camera  gameCamera           = Camera.main;
        Vector3 cursorWorldPosition  = gameCamera.ScreenToWorldPoint( new Vector3(cursorScreenPosition.x, cursorScreenPosition.y, 10f));
        var n = Vector3.up;
        var x = new Vector3(0, planeHeight, 0);
        var x0 = gameCamera.transform.position;
        var m = (cursorWorldPosition - x0).normalized;
        var h = Vector3.Dot(n, x);
        var intersectPoint = x0 + ((h - Vector3.Dot(n, x0)) / (Vector3.Dot(n, m))) * m;
        return intersectPoint;
    }
}
