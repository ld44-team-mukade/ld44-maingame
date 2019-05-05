using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private ShipId _spawned;

    private Transform _spawnInto;

    [SerializeField]
    private float _spawntime;

    private float timeElapsed;

    [SerializeField]
    private int _spawnlimit;

    private GameSpace _gameSpace;

    void Awake(){
    }

    void Start()
    {
        _spawnInto = GameManager.main.shipPool;
        _gameSpace = GameManager.main.gameSpace;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed >= _spawntime)
        {
            SpawnCheck();

            if(SpawnCheck() < _spawnlimit)
            {
                Spawn();
            }
            timeElapsed = 0.0f;
        }


    }

    public int SpawnCheck()
    {
        int ObjCount = _spawnInto.childCount - 1;

        return ObjCount;

    }

    public void Spawn(){
        var spawnedInstance = Instantiate(_spawned, transform);
        if(_spawnInto){
            spawnedInstance.transform.parent = _spawnInto;
        }else{
            spawnedInstance.transform.parent = null;
        }
        spawnedInstance.GetComponent<EnemyShipController>().gameSpace = _gameSpace;
        GameManager.main.RegisterShip(spawnedInstance);
    }
}
