using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int puntosTotales;

    void CogerPuntaje()
    {
        puntosTotales = Convert.ToInt32(FindObjectOfType<LogicaGenerador>().textoPuntuacion.text);
    }

    public void GuardarPuntaje()
    {
        CogerPuntaje();
        PlayerPrefs.SetInt("PuntajeTotal", puntosTotales);
        Debug.Log("Puntaje total: " + puntosTotales);
    }

    public void BorrarPuntaje()
    {
        PlayerPrefs.DeleteKey("PuntajeTotal");
        Debug.Log("Puntaje total: " + puntosTotales);
    }

    public void CambiarEscena (int numeroEscena) 
    {
         SceneManager.LoadScene(numeroEscena);
    }
}
