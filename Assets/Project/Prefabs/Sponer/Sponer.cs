using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponer : MonoBehaviour
{
    [SerializeField]
    private GameObject _sponed;

    [SerializeField]
    private Transform _sponeInto;

    void Awake(){

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
