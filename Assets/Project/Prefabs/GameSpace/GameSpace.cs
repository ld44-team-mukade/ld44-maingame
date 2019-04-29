using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSpace : MonoBehaviour
{
    public float radius = 1000f;

    [HideInInspector]
    public List<float> heights;

    void Awake(){
        var planes = GetComponentsInChildren<Transform>().ToList();
        planes.RemoveAt(0);
        planes.Sort((a, b)=>(int)Mathf.Sign(a.transform.position.y - b.transform.position.y));
        foreach(var plane in planes){
            plane.GetComponent<Collider>().enabled = false;
            Debug.Log(plane.transform.position.y);
        }
        heights = planes.Select(transform => transform.position.y).ToList();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
