using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponer : MonoBehaviour
{
    [SerializeField]
    private ShipId _sponed;

    [SerializeField]
    private Transform _sponeInto;

    [SerializeField]
    private float _sponetime;

    private float timeElapsed;

    [SerializeField]
    private int _sponelimit;

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

        if(timeElapsed >= _sponetime)
        {
            SponeCheck();

            if(SponeCheck() < _sponelimit)
            {
                Spone();
            }
            timeElapsed = 0.0f;
        }


    }

    public int SponeCheck()
    {
        int ObjCount = shipObj.transform.childCount - 1;

        return ObjCount;

    }

    public void Spone(){
        var sponedInstance = Instantiate(_sponed, transform);
        if(_sponeInto){
            sponedInstance.transform.parent = _sponeInto;
        }else{
            sponedInstance.transform.parent = null;
        }
        sponedInstance.GetComponent<EnemyShipController>().gameSpace = _gameSpace;
        GameManager.main.RegisterShip(sponedInstance);
    }
}
