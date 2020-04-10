using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaSubirSuelo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SubirSuelo();
    }

    public void SubirSuelo()
    {
        transform.position = new Vector3(4.45f, LogicaTetronimos.suelo-1.5f, 0);
    }
}
