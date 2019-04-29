using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockonCursor : MonoBehaviour
{
    [SerializeField]
    FCS _fcs;

    [SerializeField]
    Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _fcs.transform.position;
        var currentTarget = _fcs.CurrentTarget();
        if(currentTarget){
            _renderer.transform.position = currentTarget.position;
            _renderer.enabled = true;
        }else{
            _renderer.enabled = false;
        }
    }
}
