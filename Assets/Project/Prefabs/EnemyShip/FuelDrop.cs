using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fuelTankItem;

    [SerializeField]
    private float dropPos;

    private int num;
    private bool dropItem = false ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float _remaining = this.gameObject.GetComponent<FuelTank>().Remaining();

        if (_remaining <= 0 && dropItem == false)
        {
            ItemSpawn();
        }

    }

    void ItemSpawn()
    {
        Vector3 itemPos = transform.position;
        num = Random.Range(0, fuelTankItem.Length);
        Instantiate(fuelTankItem[num], transform.position, transform.rotation);
        dropItem = true;
    }
}
