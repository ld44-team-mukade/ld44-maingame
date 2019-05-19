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

    private float _fuelAmount;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = Camera.main.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        _itemGetAnime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _fuelIncText.text = "+" + (_fuelTank.GetFuelAmount()).ToString();
    }

    public void ItemGetAnime()
    {
        _itemGetAnime.SetTrigger("ItemGet");
        Debug.Log(_itemGetAnime);
        Debug.Log("PlayAnimation");
    }

}
