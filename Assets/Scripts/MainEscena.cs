using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainEscena : MonoBehaviour
{
    [SerializeField]
    private Button añadePalabraButton, quitaPalabraButton, examenButton, opcionesButton, exitButton;

    [SerializeField]
    private TMPro.TextMeshProUGUI debugerTxt;

    private void Awake()
    {
        Diccionario.instance.SetDebuger(debugerTxt);
        Init();
    }

    private void Init()
    {
        añadePalabraButton.onClick.AddListener(
            delegate()
            {   
                GameManager.instance.LoadScene(1);
            });
        quitaPalabraButton.onClick.AddListener(
            delegate()
            {
                if (Diccionario.instance.diccionario.Count > 0)
                    GameManager.instance.LoadScene(4);
                else
                    Diccionario.instance.MuestraMsg("No hay palabras para quitar");
            });
        examenButton.onClick.AddListener(
            delegate () 
            {
                if (Diccionario.instance.diccionario.Count > 0)
                    GameManager.instance.LoadScene(2);
                else
                    Diccionario.instance.MuestraMsg("No hay palabras para hacer un examen");
            });
        opcionesButton.onClick.AddListener(
            delegate ()
            {
                GameManager.instance.LoadScene(3);
            });
        exitButton.onClick.AddListener(
            delegate ()
            {
                Diccionario.instance.MuestraMsg("Se está cerrando app");
                GameManager.instance.CloseApp();
            });

    }
}
