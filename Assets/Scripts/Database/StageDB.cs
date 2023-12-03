using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StageData
{
    public float bpm;
    public bool[] useFatterns;
    public StageData(float _bpm)
    {
        bpm = _bpm;
        useFatterns = new bool[5];
    }
}
public class FatternProbabilityData
{
    public int[] fatternsProbability;
    public FatternProbabilityData()
    {
        fatternsProbability = new int[5];
    }
}
public class StageDB : MonoBehaviour,ICSVRead
{
    public static Dictionary<int, StageData> stageData { get; private set; }
    public static Dictionary<int,FatternProbabilityData> probabilityData { get; private set; }
    void Start()
    {
        stageData= new Dictionary<int, StageData>(10);
        probabilityData = new Dictionary<int, FatternProbabilityData>(10);
        ReadCSV("StageData");
    }

    public void ReadCSV(string _file)
    {
        string[] lines = CSVReader.LineSplit(_file);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], CSVReader.SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;
            stageData.Add(CSVReader.GetIntData(values[0]), new StageData(CSVReader.GetFloatData(values[1])));
            for (int j = 0; j < 5; j++)
            {
                stageData[CSVReader.GetIntData(values[0])].useFatterns[j] = CSVReader.GetBoolData(values[j + 2]);
            }
            probabilityData.Add(CSVReader.GetIntData(values[0]), new FatternProbabilityData());
            for (int j = 0; j < 5; j++)
            {
                probabilityData[CSVReader.GetIntData(values[0])].fatternsProbability[j] = CSVReader.GetIntData(values[j+7]);
            }
        }
    }
}
