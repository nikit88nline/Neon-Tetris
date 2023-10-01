using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicaTetrominos : MonoBehaviour
{
    private float tiempoAnterior;
    public float tiempoCaida = 0.8f;

    // Tamaño del escenario
    public static int alto = 20;
    public static int ancho = 10;

    // Punto de rotacion de la pieza
    public Vector3 puntoRotacion;

    // Posiciones ocupadas para saber si hay una linea hecha
    public static Transform [,] grid = new Transform [ancho, alto];

    public static int puntos = 0;

    public int mejoraPuntos = 1;

    public int puntosPorLinea;

    public static int velocidadPiezas = 0;

    public Text textoPuntos;

    void Start()
    {
        puntosPorLinea = FindObjectOfType<LogicaGenerador>().puntosPorLinea;
        textoPuntos = FindObjectOfType<LogicaGenerador>().textoPuntuacion;
    }

    void Update()
    {
        if(!(FindObjectOfType<LogicaPausa>().pausado))
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3 (-1,0,0);

                if(!Limites())
                {
                    transform.position -= new Vector3 (-1,0,0);
                }
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3 (1,0,0);

                if(!Limites())
                {
                    transform.position -= new Vector3 (1,0,0);
                }
            }

            // Tiempo que ha transcurrido - variable tiempo anterior (que es cero) > tiempo de caida
            if((Time.time - tiempoAnterior) > (Input.GetKey(KeyCode.DownArrow) ? tiempoCaida / 20 : tiempoCaida))
            {
                transform.position += new Vector3 (0,-1,0);

                if(!Limites())
                {
                    transform.position -= new Vector3 (0,-1,0);

                    AñadirAlGrid();

                    RevisarLineas();

                    this.enabled = false;

                    FindObjectOfType<LogicaGenerador>().NuevoTetromino();
                }

                tiempoAnterior = Time.time;
            }

            // Al pulsar, la pieza rotara respecto al punto de rotación indicado 90 grados en sentido antihorario
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(puntoRotacion), new Vector3 (0,0,1), -90);
                if(!Limites())
                {
                    transform.RotateAround(transform.TransformPoint(puntoRotacion), new Vector3 (0,0,1), 90);
                }
            }
        }

        AumentarNivel();
        AumentarVelocidad();
    }

    bool Limites()
    {
        foreach(Transform hijo in transform) 
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            if(enteroX < 0 || enteroX >= ancho || enteroY < 0 || enteroY >= alto)
            {
                return false;
            }

            if(grid [enteroX,enteroY] != null)
            {
                return false;
            }
        }

        return true;
    }

    // La pieza se añade al grid
    void AñadirAlGrid()
    {
        foreach(Transform hijo in transform) 
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            grid[enteroX,enteroY] = hijo;

            if(enteroY >= 19)
            {
                // Condicion de derrota
                puntos = 0;
                velocidadPiezas = 0;
                tiempoCaida = .8f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void RevisarLineas()
    {
        for(int i = alto - 1; i >= 0; i--) 
        {
            if(TieneLinea(i))
            {
                BorrarLinea(i);
                BajarLinea(i);
            }
        }
    }

    bool TieneLinea (int i)
    {
        for(int j = 0; j < ancho; j++) 
        {
            if(grid[j,i] == null)
            {
                return false;
            }
        }

        puntos += (puntosPorLinea * mejoraPuntos);
        textoPuntos.text = puntos.ToString();
        Debug.Log(puntos);
        return true;
    }

    void BorrarLinea (int i)
    {
        for(int j = 0; j < ancho; j++) 
        {
            Destroy(grid[j,i].gameObject);
            grid[j,i] = null;
        }
    }

    void BajarLinea (int i)
    {
        for(int y = i; y < alto; y++) 
        {
            for(int j = 0; j < ancho; j++) 
            {
                if(grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j,y];
                    grid[j,y] = null;
                    grid[j, y - 1].transform.position -= new Vector3 (0,1,0);
                }
            }
        }
    }

    void AumentarNivel()
    {
        switch (puntos) 
        {
            case 1000:
                velocidadPiezas = 1;
                break;
            case 5000:
                mejoraPuntos = 2;
                break;
            case 10000:
                velocidadPiezas = 2;
                break;
            case 20000:
                velocidadPiezas = 3;
                break;
            case 40000:
                mejoraPuntos = 3;
                break;
            case 80000:
                velocidadPiezas = 4;  
                break;
            case 200000:
                mejoraPuntos = 4;
                break;
        }
    }

    void AumentarVelocidad()
    {
       switch (velocidadPiezas) 
       {
        case 1:
            tiempoCaida = 0.35f;
            break;
        case 2:
            tiempoCaida = 0.3f;
            break;
        case 3:
            tiempoCaida = 0.2f;
            break;
        case 4:
            tiempoCaida = 0.1f;
            break;
       }
    }
}
