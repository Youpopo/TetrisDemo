using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] Tetraminos;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine("Delay");
        Spawn();
    }

    

    public void Spawn(){
        Instantiate(Tetraminos[Random.Range(0,Tetraminos.Length)],transform.position,Quaternion.identity);
    }
}
