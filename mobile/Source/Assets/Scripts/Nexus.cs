using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PanelData
{
    public int index;
    public string data;
}

[Serializable]
public class LightData
{
    public int index;
    public float r;
    public float g;
    public float b;
    public float a;
}

public class Nexus : MonoBehaviour
{
    public List<GameObject> Panels = new List<GameObject>();
    public List<GameObject> Lights = new List<GameObject>();
    private PanelData panelpacket = new PanelData();
    private LightData lightpacket = new LightData();
    private Texture2D texture = null;
    private byte[] raw = null;
    private Color chroma = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    public void SetPanelData(string json)
    {
        panelpacket = JsonUtility.FromJson<PanelData>(json);
        if (panelpacket.data.Contains("base64,")) { panelpacket.data = panelpacket.data.Split(',')[1]; }
        raw = System.Convert.FromBase64String(panelpacket.data);
        texture = new Texture2D(1, 1);
        texture.LoadImage(raw);
        Panels[panelpacket.index].GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
        Panels[panelpacket.index].GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", texture);
    }

    public void SetLightData(string json)
    {
        lightpacket = JsonUtility.FromJson<LightData>(json);
        chroma = new Color(lightpacket.r, lightpacket.g, lightpacket.b, 1.0f);
        Lights[lightpacket.index].GetComponent<Light>().color = chroma;
        Lights[lightpacket.index].GetComponent<Light>().intensity = lightpacket.a * 4.0f;
    }
}
