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
    private float _scaleSize = 1.2f;

    [SerializeField]
    private List<Renderer> _ringRenderers;

    private Vector3 _ringScales;

    private Vector3 _startValue;

    void Start()
    {
        // _ringRenderers = GetComponentsInChildren<Renderer>().ToList();
        // _ringRenderers.RemoveAt(0);
        _startValue = _ringRenderers[1].transform.localScale;
        _ringScales = new Vector3(_scaleSize, 1f, _scaleSize);
}

    // Update is called once per frame
    void Update()
    {
        if(!shipController)return;
        transform.position = shipController.transform.position;
        // shipController.currentHeightIndex;
        foreach(var renderer in _ringRenderers){
            renderer.material = white;
            renderer.transform.localScale = _startValue;
        }

        _ringRenderers[shipController.currentHeightIndex].transform.localScale = Vector3.Scale(_ringRenderers[shipController.currentHeightIndex].transform.localScale, _ringScales);
        _ringRenderers[shipController.currentHeightIndex].material = blue;
    }
}
