using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeightIndicator : MonoBehaviour
{
    [SerializeField]
    private PlayerShipController shipController;
    // Start is called before the first frame update

    [SerializeField]
    private Material white;

    [SerializeField]
    private Material blue;

    [SerializeField]
    private List<Renderer> _ringRenderers;


    void Start()
    {
        // _ringRenderers = GetComponentsInChildren<Renderer>().ToList();
        // _ringRenderers.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!shipController)return;
        transform.position = shipController.transform.position;
        // shipController.currentHeightIndex;
        foreach(var renderer in _ringRenderers){
            renderer.material = white;
        }
        _ringRenderers[shipController.currentHeightIndex].material = blue;
    }
}
