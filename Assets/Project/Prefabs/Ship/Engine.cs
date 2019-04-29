using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public float targetPower = 0;
    public AK.Wwise.RTPC EnginePowerRTPC;
    public float currentPower = 0;
    public float powerDelta = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPower = Mathf.MoveTowards(currentPower, targetPower, powerDelta);
        if(EnginePowerRTPC != null)
        {
            EnginePowerRTPC.SetGlobalValue(currentPower);
        }
    }
}
