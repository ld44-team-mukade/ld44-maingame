using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponer : MonoBehaviour
{
    [SerializeField]
    private GameObject _sponed;

    [SerializeField]
    private Transform _sponeInto;

    [SerializeField]
    private float _sponetime;

    private float timeElapsed;

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
            Spone();
            Debug.Log("Spone");
            timeElapsed = 0.0f;
        }


    }

    public void Spone(){
        var sponedInstance = Instantiate(_sponed, transform);
        if(_sponeInto){
            sponedInstance.transform.parent = _sponeInto;
        }else{
            sponedInstance.transform.parent = null;
        }
    }
}
