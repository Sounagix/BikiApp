using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesEscena : MonoBehaviour
{
    [SerializeField]
    private Button backButton, creaNuevosDatosButton, siButton, noButton;

    [SerializeField]
    private GameObject seguroPanel;

    private void Awake()
    {
        backButton.onClick.AddListener(
            delegate()
            {
                GameManager.instance.LoadScene(0);
            });
        creaNuevosDatosButton.onClick.AddListener(
             delegate ()
             {
                 seguroPanel.SetActive(true);
                 SetUpSiNoButtons();    
             });
    }

    private void SetUpSiNoButtons()
    {
        siButton.onClick.AddListener(
            delegate ()
            {
                Diccionario.instance.CreaNuevosDatos();
                seguroPanel.SetActive(false);
            });
        noButton.onClick.AddListener(
            delegate ()
            {
                seguroPanel.SetActive(false);
            });
    }
}
