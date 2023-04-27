using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Tour_Script : MonoBehaviour
{
    public GameObject tourN;

    public Renderer rendererTour;

    public DonneesTourNord1 donnees;

    public TMP_Text texte;

    public Slider sliderTemps;

    public Toggle typeVisu;

    public bool typeVisubool = true;
    public Toggle defileTemps;

    public bool defileTempsBool = false;

    public Text textVisu;

    public TextAsset file;

    public bool actifText;

    public TemperatureData currentTemp;

    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        sliderTemps.onValueChanged.AddListener(OnSliderValueChanged);
        typeVisu.onValueChanged.AddListener(OnToggleValueChanged);
        textVisu.text = "Température";
        defileTemps.onValueChanged.AddListener(OnToggleValueChanged2);
        rendererTour = tourN.GetComponent<Renderer>();
        donnees = new DonneesTourNord1(this.file);
        Debug.Log("Construction done");
        this.OnSliderValueChanged(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (sliderTemps.value < 0.99f && defileTempsBool)
        {
            sliderTemps.value += 0.0001f;
        }
    }

    public Color calculColor(TemperatureData temperature)
    {
        float maxTemp = 55f;
        float minTemp = -10f;
        float tempForCalc = temperature.TempSensor - minTemp;
        float maxHum = 100f;
        float minHum = 15f;
        float humForCalc = temperature.HumiditeSensor - minTemp;
        float calc = 0f;
        float calcOppose = 0f;
        if (typeVisubool)
        {
            calc = (255f / maxTemp) * (tempForCalc / 255f);
            calcOppose = 1f - calc;
        }
        else
        {
            calc = (255f / maxHum) * (humForCalc / 255f);
            calcOppose = 1f - calc;
        }
        Color resultat = new Color(calc, 0f, calcOppose);
        return resultat;
    }

    private void OnSliderValueChanged(float value)
    {
        int total = donnees.temperatureDataList.Count;
        float toUse = (float)total * value;
        currentTemp = donnees.temperatureDataList[(int)toUse];
        Color visual = calculColor(currentTemp);
        rendererTour.material.SetColor("_Color",visual);
    }

    private void OnToggleValueChanged(bool val)
    {
        typeVisubool = val;
        this.OnSliderValueChanged(sliderTemps.value);
        if (typeVisubool)
        {
            textVisu.text = "Température";
        }
        else
        {
            textVisu.text = "Humidité";
        }
    }

    private void OnToggleValueChanged2(bool val)
    {
        defileTempsBool = val;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UI.GetComponent<Text_UI>().UpdateTourActive(this.tourN);
    }
}
