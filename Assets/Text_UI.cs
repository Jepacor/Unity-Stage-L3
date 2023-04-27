using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_UI : MonoBehaviour
{
    public TMP_Text texte;

    public GameObject[] tours;

    public Tour_Script[] ScriptsTours;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tours.Length; i++)
        {
            ScriptsTours[i] = tours[i].GetComponent<Tour_Script>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tours.Length; i++)
        
            if (ScriptsTours[i].actifText)
            {
                texte.text = $"Capteur : {ScriptsTours[i].name} \n" +
                             $"Date : {ScriptsTours[i].currentTemp.Timestamp.ToString("g")} \n" +
                             $"Température : {ScriptsTours[i].currentTemp.TempSensor} °C \n" +
                             $"Humidité : {ScriptsTours[i].currentTemp.HumiditeSensor} %";
            }
    }

    public void UpdateTourActive(GameObject nouvelleTour)
    {
        for (int i = 0; i < tours.Length; i++)
        {
            if (nouvelleTour.Equals(tours[i]))
            {
                ScriptsTours[i].actifText = true;
            }
            else
            {
                ScriptsTours[i].actifText = true;
            }
        }
    }
}

