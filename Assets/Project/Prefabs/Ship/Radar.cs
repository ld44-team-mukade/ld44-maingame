using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Radar : MonoBehaviour
{
    [HideInInspector]
    public List<ShipId> nearShips;

    [SerializeField]
    private float _distance = 200f;

    public float GetDistance(){
        return _distance;
    }

    void Awake(){
        nearShips = new List<ShipId>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // nearShips.Where(shipId => shipId.BroadcastMessage[] );
    }
}
