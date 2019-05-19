using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetInfo : MonoBehaviour
{
    [SerializeField]
    TextMesh _fuelIncText;

    [SerializeField]
    FuelTank _fuelTank;

    [SerializeField]
    Animator _itemGetAnime;

    private bool _itemGetTrigger = false;

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

        _fuelIncText.text = "+" + (_fuelTank.GetFuelAmount()).ToString();
        
        if (_fuelTank.ItemGetCheck() == true) 
        {
            Debug.Log("Test");
            _itemGetAnime.SetTrigger("ItemGet");
        }
    }

}
