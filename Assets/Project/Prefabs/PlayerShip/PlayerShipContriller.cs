using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipContriller : MonoBehaviour
{
    [SerializeField]
    ShipMovement _shipMovement;
    [SerializeField]
    int currentHeightIndex = 0;

    [SerializeField]
    public List<int> heights;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int direction = 0;
        if(Input.GetKeyDown(KeyCode.Q)){direction += 1;};
        if(Input.GetKeyDown(KeyCode.E)){direction -= 1;};

        var targetHeightIndex = Mathf.Clamp(currentHeightIndex+direction, 0, heights.Count-1);
        if(targetHeightIndex != currentHeightIndex){
            var isSuccessed = TryToChangeHeight(targetHeightIndex);
            if(isSuccessed){
                currentHeightIndex = targetHeightIndex;
            }
            Debug.Log(currentHeightIndex);
        }
    }

    bool TryToChangeHeight(int index){
        var currentAgentPosition = _shipMovement.CurrentAgentPosition();
        var targetPosition = currentAgentPosition; targetPosition.y = heights[index];
        var direction = (targetPosition - currentAgentPosition).normalized;

        var ray = new Ray(currentAgentPosition, Vector3.up * direction.y);
        RaycastHit hit;
        bool changeable = true;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
            changeable = hit.collider.tag != "Obstacle";
        }
        
        if(!changeable)return false;
        _shipMovement.ChangeCurrentAgentPosition(targetPosition);
        // Debug.Log(targetPosition);
        return true;
    }
}
