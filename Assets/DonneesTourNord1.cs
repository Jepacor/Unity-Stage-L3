using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class TemperatureData
{
    public DateTime Timestamp { get; set; }
    public float TempSensor { get; set; }
    public float HumiditeSensor { get; set; }
}

public class DonneesTourNord1
{
    public List<TemperatureData> temperatureDataList;
    public TextAsset file;

    public DonneesTourNord1(TextAsset file)
    {
        
        //string filePath = "/Assets/Donnees/DATA_TERRAIN_N2N.csv";
        this.file = file;

        temperatureDataList = ReadTemperatureDataFromCSV();

        foreach (var data in temperatureDataList)
        {
            Debug.Log($"Timestamp: {data.Timestamp}, N1NTemp: {data.TempSensor}");
        }
    }

    public List<TemperatureData> ReadTemperatureDataFromCSV()
    {
        List<TemperatureData> temperatureDataList = new List<TemperatureData>();

        //String fileData = System.IO.File.ReadAllText(filePath);
        //Skip la 1re ligne
        int firstNewLineIndex = file.text.IndexOf("\n");
        
        string remainingText = file.text.Substring(firstNewLineIndex + Environment.NewLine.Length);
        //string[] lines = remainingText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
       
        String[] lines = remainingText.Split("\n"[0]);
        int i = 0;
        
        foreach (var line in lines)
        {
            if ( line.Equals("") )
            {
                //skip la dernière ligne
            }
            else
            {
                string[] values = line.Split(',');
                i++;
                string dateStr = values[0];
                string timeStr = values[1];
                string dateTimeStr = $"{dateStr} {timeStr}";
                DateTime dateTime = DateTime.ParseExact(dateTimeStr, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                try
                {
                    float temp = float.Parse(values[2], CultureInfo.InvariantCulture);
                    float Humidite = float.Parse(values[3], CultureInfo.InvariantCulture);
                    TemperatureData data = new TemperatureData { Timestamp = dateTime, TempSensor = temp, HumiditeSensor = Humidite};
                    temperatureDataList.Add(data);
                }
                catch (Exception e)
                {
                    Debug.Log($"Erreur ligne : {i} ");
                    //TemperatureData data = new TemperatureData { Timestamp = dateTime, N1NTemp = 15f };
                }
                
            }
        }
        return temperatureDataList;
    }
}