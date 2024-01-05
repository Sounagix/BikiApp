using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamenEscena : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI preguntaText, timerTxt;

    [SerializeField]
    private TMPro.TMP_InputField inputField;

    private Stack<KeyValuePair<string, string>> pila = new Stack<KeyValuePair<string, string>>();

    private string respuesta = "";

    private int correctas = 0, total = 0;

    private float tiempoActual, tiempoInicial;

    private bool cuenta = true;

    [SerializeField]
    private Button backButton;

    private void Start()
    {
        init();
        MuestraPregunta();
        tiempoInicial = Time.time;
    }

    private void Update()
    {
        int t = (int)tiempoActual;
        if (cuenta)
            tiempoActual = Time.time - tiempoInicial;
        if (cuenta && t < (int)tiempoActual)
        {
            timerTxt.text = ((int)tiempoActual).ToString(); 
        }
    }

    private void init()
    {
        backButton.onClick.AddListener(
            delegate()
            {
                GameManager.instance.LoadScene(0);
            }
            );
        inputField.onEndEdit.AddListener(OnInputFieldEndEdit);

        total = Diccionario.instance.diccionario.Count;
        for (int i = 0; i < total; i++)
        {
            int index = 0;
            int rnd = Random.Range(0,Diccionario.instance.diccionario.Count - 1);
            foreach(var par in Diccionario.instance.diccionario)
            {
                if (index == rnd)
                {
                    pila.Push(new KeyValuePair<string, string>(par.Key, par.Value));
                    break;
                }
                index++;
            }
            Diccionario.instance.diccionario.Remove(pila.Peek().Key);
        }
        Diccionario.instance.CargaPalabras();
    }

    private void OnInputFieldEndEdit(string text)
    {
        if(respuesta.Equals(text))
        {
            correctas++;
            inputField.image.color = Color.green;
            inputField.text = "";
            Invoke(nameof(BackColor), 1.0f);
        }
        else
        {
            inputField.image.color = Color.red;
            inputField.text = respuesta;
            Invoke(nameof(BackColor), 1.0f);
        }
    }

    private void BackColor()
    {
        inputField.image.color = Color.white;
        MuestraPregunta();
    }


    private void MuestraPregunta()
    {
        if (pila.Count > 0)
        {
            var par = pila.Pop();
            preguntaText.text = par.Key;
            respuesta = par.Value;
        }
        else
        { 
            cuenta = false;
            preguntaText.text = correctas + " / " + total;
            InvokeRepeating(nameof(PauseTiempo), 0.0f, 1.0f);
        }
    }

    private void PauseTiempo()
    {
        if (timerTxt.text == "")
        {
            timerTxt.text = ((int)tiempoActual).ToString();
        }
        else
        {
            timerTxt.text = "";
        }
    }
}
