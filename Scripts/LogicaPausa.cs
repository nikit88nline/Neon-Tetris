using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPausa : MonoBehaviour
{
    public GameObject panelPausa;

    public bool pausado;

    void Start()
    {
        pausado = false;
        panelPausa.SetActive(false);
    }

    public void PausarJuego () 
    {
        if(!pausado)
        {
            Time.timeScale = 0;
            pausado = true;
            panelPausa.SetActive(true);
        }

        else
        {
            Time.timeScale = 1;
            pausado = false;
            panelPausa.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            PausarJuego();
        }
    }
}
