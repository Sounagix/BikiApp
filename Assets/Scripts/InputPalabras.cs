using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPalabras : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_InputField inputField;

    [SerializeField]
    private TMPro.TextMeshProUGUI placeHolderText, indicacionesTxt, debugTxt;

    [SerializeField]
    private Button backButton; 

    private string key = "";

    private string value = "";


    private void Awake()
    {
        backButton.onClick.AddListener(
            delegate()
            {
                GameManager.instance.LoadScene(0);
            });
        // Asociamos los m�todos a los eventos del InputField
        if (inputField != null)
        {
            indicacionesTxt.text = "Ingrese una palabra en espa�ol";
            inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
            //inputField.onSelect.AddListener(
            //    delegate(string text) 
            //    {
            //        placeHolderText.text = "";
            //    });
        }
        Diccionario.instance.SetDebuger(debugTxt);
    }

    private void OnInputFieldEndEdit(string text)
    {
        // Este m�todo se ejecuta cuando el usuario presiona Enter o sale del InputField
        if (key == "" && !Diccionario.instance.diccionario.ContainsKey(text))
        {
            key = text.ToLower();
            inputField.text = "";
            indicacionesTxt.text = "Escribe palabra en ingles";
        }
        else if(key != "" && !Diccionario.instance.diccionario.ContainsValue(text))
        {
            value = text.ToLower();
            indicacionesTxt.text = "Ingrese otra palabra en espa�ol";
            inputField.text = "Palabra ingresada correctamente";
            Diccionario.instance.A�adePalabra(key,value);
            key = "";
            value = "";
        }
        else
        {
            indicacionesTxt.text = "Ingrese otra palabra en espa�ol, esta ya existe";
            placeHolderText.text = "Ya existe la palabra";
            key = "";
            value = "";
        }
    }
}
