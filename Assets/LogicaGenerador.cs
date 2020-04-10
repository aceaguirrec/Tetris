using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaGenerador : MonoBehaviour
{
    public GameObject[] tetronimos;

    // Start is called before the first frame update
    void Start()
    {
        NuevoTetronimo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NuevoTetronimo()
    {
        Instantiate(tetronimos[Random.Range(0, tetronimos.Length)], transform.position, Quaternion.identity);
    }
}
