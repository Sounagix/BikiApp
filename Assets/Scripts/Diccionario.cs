using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Diccionario : MonoBehaviour
{
    public static Diccionario instance;

    public Dictionary<string, string> diccionario = new Dictionary<string, string>();

    private string nombreTxt = "palabras.txt";

    private TMPro.TextMeshProUGUI text;

    private Stack<string> pilaMensajes = new Stack<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    public void BorraDatos()
    {
        diccionario.Clear();
        if (File.Exists(nombreTxt))
        {
            File.Delete(nombreTxt);
            MuestraMsg("Se ha eliminado los datos");
        }
    }

    public void CargaPalabras()
    {
        Init();
    }

    public void CreaNuevosDatos()
    {
        BorraDatos();
        Init();
    }

    private void Init()
    {
        try
        {
            if (File.Exists(nombreTxt))
            {
                StreamReader streamReader = new StreamReader(nombreTxt);
#if UNITY_EDITOR	
                print(Path.GetFullPath(nombreTxt));
#endif
                while (!streamReader.EndOfStream)
                {
                    string[] cadenas = streamReader.ReadLine().Split("=");
                    string key = cadenas[0];
                    string value = cadenas[1];
                    diccionario.Add(key, value);
                }
                MuestraMsg("Se han cargado " + diccionario.Count + " palabras");
                streamReader.Close();
            }
            else
            {
                File.CreateText(nombreTxt);
                MuestraMsg("Se ha creado un diccionario de palabras en " + Path.GetFullPath(nombreTxt));
            }
        }
        catch (Exception e)
        {
            MuestraMsg(e.Message);
        }
    }

    public bool AñadePalabra(string key, string value)
    {
        if (diccionario.ContainsKey(key) || diccionario.ContainsValue(value))
        {
            return false;
        }
        else
        {
            diccionario.Add(key,value);
            ActualizaTxt();
            return true;
        }
    }

    public bool EliminaPalabra(string key)
    {
        if (diccionario.ContainsKey(key))
        {
            diccionario.Remove(key);
            ActualizaTxt();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ActualizaTxt()
    {
        if (File.Exists(nombreTxt))
        {
            using (StreamWriter writer = new StreamWriter(nombreTxt))
            {
                foreach(var par in diccionario)
                {
                    writer.WriteLine(par.Key + "=" + par.Value);
                }
                writer.Close();
            }
            MuestraMsg("Se han actualizado las palabras");
        }
    }

    public void SetDebuger(TMPro.TextMeshProUGUI _text)
    {
        text = _text;
        if (pilaMensajes.Count > 0)
        {
            MuestraMensajePila();
        }
    }

    private void MuestraMensajePila()
    {
        if (pilaMensajes.Count > 0)
        {
            string msg = pilaMensajes.Pop();
            MuestraMsg(msg);
            if (pilaMensajes.Count > 0)
                Invoke(nameof(MuestraMensajePila), 3.0f);
        }
    }

    public void MuestraMsg(string msg)
    {
        if (text != null)
        {
            text.text = msg;
            Invoke(nameof(LimpiaDebug),2.0f);
        }
        else
        {
            pilaMensajes.Push(msg);
        }
    }

    private void LimpiaDebug()
    {
        if (text != null)
        {
            text.text = pilaMensajes.Count == 0 ? "v 1.0" : "esperando siguiente mensaje";
        }
    }
}
