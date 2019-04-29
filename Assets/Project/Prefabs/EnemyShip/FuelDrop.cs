using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject fuelTankItem;

    [SerializeField]
    private float dropPos;

    private bool dropItem = false ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float _remaining = this.gameObject.GetComponent<FuelTank>().Remaining();
        Vector3 itemPos = transform.position;

        if (_remaining <= 0 && dropItem == false)
        {
            Debug.Log("OK");
            itemPos.x += dropPos;

            Instantiate(fuelTankItem, itemPos, transform.rotation);

            dropItem = true;
        }

    }
}
