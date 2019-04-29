using System.Collections;
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

    void Awake(){
        _cursorInstance = Instantiate(_cursorPrefab);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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

        var cursorPositionOnPlane = CursorPosition(gameSpace.heights[currentHeightIndex]);
        _cursorInstance.transform.position = cursorPositionOnPlane;
        if(Input.GetMouseButtonDown(0)){
            var isReachable = _shipMovement.IsReachable(cursorPositionOnPlane);
            if(isReachable){
                _shipMovement.ChangeTargetAgentPosition(cursorPositionOnPlane);
            }
        };
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
