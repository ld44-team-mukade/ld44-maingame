using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager main;

    [SerializeField]
    private Transform _playerShip;

    [SerializeField]
    private Transform spawnerPool;

    [SerializeField]
    public Transform shipPool;
    private Dictionary<int, ShipId> _shipIdDict;

    [SerializeField]
    public GameSpace gameSpace;
    private List<Transform> _spawner;
    // Start is called before the first frame update
    private int _idCounter = 0;

    private float _startTime;

    void Awake(){
        main = this;
        _shipIdDict = new Dictionary<int, ShipId>();
        _idCounter = 0;
    }
    void Start()
    {
        _startTime = Time.time;
        SetupShips();
    }

    void SetupShips(){
        var shipIds = shipPool.GetComponentsInChildren<ShipId>().ToList();
        foreach (var shipId in shipIds)
        {
            RegisterShip(shipId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShipDict();
        UpdateShipRadar();

        if(!_playerShip){
            ScoreBoard.score = Mathf.FloorToInt((Time.time - _startTime)*25f);
            GoToGameOver();
        }
    }

    void UpdateShipDict(){
        var nullElemKeys = _shipIdDict.Keys.Where((key) => !_shipIdDict[key].IsLiving()).ToList();
        // var nullElemKeys = _shipIdDict.Keys.Select((key) => _shipIdDict[key]?-1:key).Where(key => key != -1).ToList();
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

    public float CurrentDuration(){
        return Mathf.FloorToInt((Time.time - _startTime));
    }

    public void RegisterShip(ShipId shipId){
        shipId.Id = _idCounter;
        _shipIdDict[_idCounter] = shipId;
        var enemyShipController = shipId.GetComponent<EnemyShipController>();
        if (enemyShipController)
        {
            enemyShipController.gameSpace = gameSpace;
        }
        _idCounter++;
    }

}
