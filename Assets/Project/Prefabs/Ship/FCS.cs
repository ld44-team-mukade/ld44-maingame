using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FCS : MonoBehaviour
{
    private List<CannonController> _cannons;

    [SerializeField]
    private Radar _radar;

    private Transform _currentTarget;

    [SerializeField]
    private FuelTank _fuelTank;
    // Start is called before the first frame update
    void Start()
    {
        _cannons = GetComponentsInChildren<CannonController>().ToList();
        // _cannons.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        var selectableShips = _radar.NearHostileShips();
        if(0 < selectableShips.Count){
            var sorted = selectableShips.OrderBy(ship => Vector3.Distance(ship.transform.position, transform.position)).ToList();
            _currentTarget = sorted[0].transform;
        }

        if(_currentTarget){
            SetTarget(_currentTarget.position);
        }
    }

    void SetTarget(Vector3 targetPosition){
        foreach(var cannon in _cannons){
            cannon.targetPosition = targetPosition;
        }
    }

    public Transform CurrentTarget(){
        return _currentTarget;
    }

    public void Fire(){
        foreach(var cannon in _cannons){
            cannon.Fire(tag);
            _fuelTank.DecrementFuel(cannon.Cost());
        }
    }
}
