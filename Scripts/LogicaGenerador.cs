using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaGenerador : MonoBehaviour
{
    public GameObject [] tetrominos;

    //Campo Proxima pieza
    public Transform transformProxPieza;
    public GameObject [] proximaPieza;
    public int numeroAleatorio;

    public int puntosPorLinea;

    public Text textoPuntuacion;

    void Start()
    {
        numeroAleatorio = Random.Range(0, tetrominos.Length);
        Instantiate(proximaPieza[numeroAleatorio], transformProxPieza.position, Quaternion.identity);
        NuevoTetromino ();
    }

    void ProximaPieza()
    {
        numeroAleatorio = Random.Range(0, tetrominos.Length);
        Instantiate(proximaPieza[numeroAleatorio], transformProxPieza.position, Quaternion.identity);
    }

    public void NuevoTetromino () 
    {
        Destroy(GameObject.FindWithTag("pantalla"));

        Instantiate(tetrominos[numeroAleatorio], transform.position, Quaternion.identity);

        ProximaPieza();
    }
}
