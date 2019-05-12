using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetInfo : MonoBehaviour
{
    [SerializeField]
    TextMesh _FuelIncText;

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

    }

    public void ItemGetAnime(float fuelamount)
    {
        _FuelIncText.text = "+" + fuelamount.ToString();

    }

}
