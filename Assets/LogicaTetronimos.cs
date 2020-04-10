using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaTetronimos : MonoBehaviour
{
    private float TiempoAnterior;
    public float TiempoCaida = 0.8f;

    public static int alto = 20;
    public static int ancho = 10;
    public static int suelo = 2;

    public Vector3 PuntoRotacion;

    private static Transform[,] grid = new Transform[ancho, alto];

    public static int puntaje = 0;
    public static int niveldeDificultad = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //se mueve a la izquierda
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            
            //No deja que pase de los limites de la pantalla
            if(!Limites())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }

        }
        //se mueve a la derecha
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            
            //no deja que pase de los limites
            if (!Limites())
            {
                transform.position -= new Vector3(1, 0, 0);
            }

        }
        
        if(Time.time-TiempoAnterior > (Input.GetKey(KeyCode.DownArrow) ? TiempoCaida / 20 : TiempoCaida))
        {
            transform.position += new Vector3(0, -1, 0);

            if (!Limites())
            {
                transform.position -= new Vector3(0, -1, 0);

                AñadirAlGrid();

                RevisasLineas();

                this.enabled = false;
                FindObjectOfType<LogicaGenerador>().NuevoTetronimo();
            }

            TiempoAnterior = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(PuntoRotacion), new Vector3(0, 0, 1), -90);
            if (!Limites())
            {
                transform.RotateAround(transform.TransformPoint(PuntoRotacion), new Vector3(0, 0, 1), 90);
                
            }
        }

        //AumetarNiever();
        if(niveldeDificultad <= 2)
        {
            AumentarDificultad();
        }
        else
        {
            Reiniciar();
        }
        
        //SubirSuelo();
    }

    bool Limites()
    {
        foreach (Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            if (enteroX < 0 || enteroX >= ancho || enteroY < suelo || enteroY >= alto)
            {
                return false;
            }
            if (grid[enteroX, enteroY] != null)
            {
                return false;
            }
        }

        return true;
    }

    void AñadirAlGrid()
    {
        foreach(Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            grid[enteroX, enteroY] = hijo;

            if(enteroY >= alto-1)
            {              
                Reiniciar();
            }

        }
    }

    void Reiniciar()
    {
        puntaje = 0;
        niveldeDificultad = 1;
        TiempoCaida = 0.8f;
        suelo = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CambiarNiver()
    {
        puntaje = 0;
        niveldeDificultad += 1;
        TiempoCaida = 0.8f;
        suelo += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void RevisasLineas()
    {
        for (int i = alto - 1; i >= 0; i--)
        {
            if(TieneLinea(i))
            {
                BorrarLinea(i);
                BajarLinea(i);
            }
        }
    }

    bool TieneLinea(int i)
    {
        for (int j = 0; j < ancho; j++)
        {
            if(grid[j,i] ==  null)
            {
                return false;
            }
        }

        puntaje += 100;
        Debug.Log(puntaje);
        return true;
    }

    void BorrarLinea(int i)
    {
        for (int j = 0; j < ancho; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void BajarLinea(int i)
    {
        for (int k = i; k < alto; k++)
        {
            for (int l = 0; l < ancho; l++)
            {
                if (grid[l, k] != null)
                {
                    grid[l, k - 1] = grid[l, k];
                    grid[l, k] = null;
                    grid[l, k - 1].transform.position -= new Vector3(0, 1, 0);
                }
                
            }
        }
    }

    void SubirSuelo()
    {
        for (int k = alto - 1; k >= 0; k--)
        {
            for (int i = k; i < alto; i++)
            {
                for (int j = 0; j < ancho; j++)
                {
                    if (grid[j, i] != null)
                    {
                        grid[j, i + 1] = grid[j, i];
                        //grid[j, i] = null;
                        grid[j, i + 1].transform.position += new Vector3(0, 1, 0);
                    }
                }
            }
        }


    }


    void AumentarDificultad()
    {

        if(puntaje >= 500)
        {
            CambiarNiver();
        }

    }

}
