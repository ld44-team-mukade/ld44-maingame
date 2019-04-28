using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform _playerShip;
    [SerializeField]
    private List<Transform> _planes;
    private List<Transform> _sponer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_playerShip){
            GoToGameOver();
        }
    }

    void GoToGameOver(){
        Debug.Log("GoToGameOver");
    }
}
