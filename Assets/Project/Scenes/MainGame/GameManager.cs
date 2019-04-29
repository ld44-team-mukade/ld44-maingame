using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform _playerShip;

    [SerializeField]
    private Transform shipPool;
    private Dictionary<int, ShipId> _shipIdDict;

    [SerializeField]
    private GameSpace _gameSpace;
    private List<Transform> _sponer;
    // Start is called before the first frame update
    private int _idCounter = 0;

    void Awake(){
        _shipIdDict = new Dictionary<int, ShipId>();
    }
    void Start()
    {
        _idCounter = 0;
        var shipIds = shipPool.GetComponentsInChildren<ShipId>().ToList();
        foreach (var shipId in shipIds)
        {
            shipId.Id = _idCounter;
            _shipIdDict[_idCounter] = shipId;
            _idCounter++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShipDict();
        UpdateShipRadar();

        if(!_playerShip){
            GoToGameOver();
        }
    }

    void UpdateShipDict(){
        var nullElemKeys = _shipIdDict.Keys.Select((key) => _shipIdDict[key]?-1:key).Where(key => key != -1).ToList();
        foreach (var nullelemKey in nullElemKeys)
        {
            _shipIdDict.Remove(nullelemKey);
        }
    }

    void UpdateShipRadar(){
        foreach(var shipId in _shipIdDict.Values)
        {
            var radar = shipId.GetComponent<Radar>();
            radar.nearShips = GetNearestShip(radar.transform.position, radar.GetDistance());
        }
    }

    void GoToGameOver(){
        SceneManager.LoadScene("GameOver");
    }

    public List<ShipId> GetNearestShip(Vector3 center, float maxDistance){
        Func<Vector3, bool> isNear = (Vector3 position) => (position-center).magnitude < maxDistance;
        return _shipIdDict.Values.Select(shipId => shipId)
                                 .Where(shipId=> isNear(shipId.transform.position))
                                 .ToList();
    }
}
