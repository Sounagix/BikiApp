using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitaPalabrasEscena : MonoBehaviour
{
    [SerializeField]
    private Button backButton, borraButton;

    [SerializeField]
    private GameObject toggle;

    [SerializeField]
    private Transform palabrasTr;

    private List<Toggle> toggles = new List<Toggle>();

    private void Awake()
    {
        foreach(var _tg in Diccionario.instance.diccionario)
        {
            Toggle tg = Instantiate(toggle,palabrasTr).GetComponent<Toggle>();
            tg.GetComponentInChildren<Text>().text = _tg.Key;
            toggles.Add(tg);
        }
        SetUp();
    }

    private void SetUp()
    {
        backButton.onClick.AddListener(
            delegate()
            {
                GameManager.instance.LoadScene(0);
            });
        borraButton.onClick.AddListener(
            delegate()
            {
                foreach (Toggle _tg in toggles)
                {
                    if (_tg.isOn)
                    {
                        Diccionario.instance.EliminaPalabra(_tg.GetComponentInChildren<Text>().text);
                    }
                }
                toggles.Clear();
                GameManager.instance.LoadScene(0);
            });
    }
}
