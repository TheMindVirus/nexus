using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PanelData
{
    public int panel;
    public string data;
}

public class Screen : MonoBehaviour
{
    public List<GameObject> Panels = new List<GameObject>();
    private PanelData packet = new PanelData();
    private Texture2D texture = null;
    private byte[] raw = null;

    void SetPanelData(string json)
    {
        packet = JsonUtility.FromJson<PanelData>(json);
        if (packet.data.Contains("base64,")) { packet.data = packet.data.Split(',')[1]; }
        raw = System.Convert.FromBase64String(packet.data);
        texture = new Texture2D(1, 1);
        texture.LoadImage(raw);
        Panels[packet.panel].GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
        Panels[packet.panel].GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", texture);
    }
}
