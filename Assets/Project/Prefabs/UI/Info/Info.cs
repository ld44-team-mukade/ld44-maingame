using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField]
    TextMesh _fuelText;

    [SerializeField]
    TextMesh _timeText;

    [SerializeField]
    FuelTank _fuelTank;

    [SerializeField]
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = Camera.main.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        _fuelText.text = Mathf.FloorToInt(_fuelTank.Remaining()).ToString();
        _timeText.text = Mathf.FloorToInt(_gameManager.CurrentDuration()).ToString();
    }
}
