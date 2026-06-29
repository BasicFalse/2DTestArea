using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogPanel : MonoBehaviour
{
    [Serializable]
    public class MessageLog { public string contents; public float lifetime; public void Changelife(float Time) => lifetime = Time; }

    public List<MessageLog> Logs;

    [SerializeField] private Text textBox;

    void Update()
    {
        float dtime = Time.deltaTime;
        textBox.text = "";
        for(int i =0;i<Logs.Count; i++)
        {
            float change = Logs[i].lifetime;
            change -= dtime;
                Logs[i].Changelife(change);
            if(change > 0)
            {
            }
            else
            {
                Logs.RemoveAt(i);
                i--;
                continue;
            }
            textBox.text += Logs[i].contents + "\n";
        }
    }

    public void AddLog(string message, float cduration = 10f)
    {
        MessageLog ml = new() {contents = message, lifetime = cduration};
        Logs.Add(ml);
    }
}
