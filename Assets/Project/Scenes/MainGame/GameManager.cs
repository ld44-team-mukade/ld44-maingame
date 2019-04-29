using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<Transform> _planes;
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
        foreach (var key in _shipIdDict)
        {
            Debug.Log(key);
        }

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

    void GoToGameOver(){
        SceneManager.LoadScene("GameOver");
    }
}
