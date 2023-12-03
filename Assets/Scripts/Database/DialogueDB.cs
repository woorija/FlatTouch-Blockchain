using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public struct DialogueData
{
    public int characterNumber { get; private set; }
    public int emoteNumber { get; private set; }
    public string dialogue { get; private set; }

    public DialogueData(int _char, int _emote, string _dialogue)
    {
        characterNumber = _char;
        emoteNumber = _emote;
        dialogue = _dialogue;
    }
}
public class DialogueDB : MonoBehaviour,ICSVRead
{
    public static Dictionary<int, DialogueData> dialogueData { get; private set; }
    public static string[] characterNames { get; private set; } = {
        "주인공",
        "토끼",
        "너구리",
        "백곰왕",
        "마녀",
        "주민들"
    };

    private void Start()
    {
        dialogueData = new Dictionary<int, DialogueData>(60);
        ReadCSV("DialogueData");
    }
    public void ReadCSV(string _file)
    {
        string[] lines = CSVReader.LineSplit(_file);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], CSVReader.SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;
            dialogueData.Add(CSVReader.GetIntData(values[0]), new DialogueData(CSVReader.GetIntData(values[1]), CSVReader.GetIntData(values[2]), CSVReader.GetStringData(values[3])));
        }
    }
}
