using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private ShipId _spawned;

    [SerializeField]
    private Transform _spawnInto;

    [SerializeField]
    private float _spawntime;

    private float timeElapsed;

    [SerializeField]
    private int _spawnlimit;

    [SerializeField]
    private GameSpace _gameSpace;

    [SerializeField]
    private GameObject shipObj;

    void Awake(){

    }

    void Start()
    {
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
        int ObjCount = shipObj.transform.childCount - 1;

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
