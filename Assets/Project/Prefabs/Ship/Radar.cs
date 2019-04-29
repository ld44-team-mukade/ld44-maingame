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

    public List<ShipId> NearHostileShips(){
        return nearShips.Where((ship) => ship.tag != tag)
                        .Where((ship) => ship.GetInstanceID() != GetInstanceID())
                        .ToList();

    }

    // Update is called once per frame
    void Update()
    {
        // nearShips.Where(shipId => shipId.BroadcastMessage[] );
    }
}
