using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class Scraper : MonoBehaviour
{
    public List<GameObject> Panels = new List<GameObject>();
    public float Interval = 60.0f;

    private float timeout = -1.0f;
    private int random = 0;
    private List<string> links = new List<string>();
    private List<string> query = new List<string>();
    private string url = "";

    private string term = "src=\"";
    private string mark = "\"";
    private string secure = "https://";
    private string check = "&amp;";

    private Texture tex = null;
    private int select = 0;
    private bool once = false;

    void Start()
    {
        Refresh(); //Initial Load-In looked a bit blank
        //Optionally Disable the In-Game Timer and Call from WebGL SendMessage
    }
///*
    void Update()
    {
        if (Time.time >= timeout) { Refresh(); timeout = Time.time + Interval; }
    }
//*/
    void Refresh()
    {
        Debug.Log("[INFO]: Refresh");
        random = Random.Range(0, 9999999);
        query = new List<string>();
        string[] tmp = { "tabletop", "game", "cover", "page", random.ToString() };
        query.AddRange(tmp);
        url = "https://www.google.com/search?&tbm=isch&q=";
        for (int i = query.Count; i > 0; --i)
        {
            url += query[query.Count - i];
            if (i > 1) { url += "+"; }
        }
        //Debug.Log("[INFO]: " + url);
        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (UnityWebRequest.Result.Success == request.result)
            {
                Process(request.downloadHandler.text);
            }
            else { Debug.Log("[WARN]: GetRequest: " + request.error); }
        }
    }

    IEnumerator DownloadImage(string uri)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri))
        {
            yield return request.SendWebRequest();
            if (UnityWebRequest.Result.Success == request.result)
            {
                tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                if (once == false) { Debug.Log("[INFO]: " + tex.ToString()); once = true; }
                select = Random.Range(0, Panels.Count);
                Panels[select].GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex);
                Panels[select].GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", tex);
            }
            else { Debug.Log("[WARN]: DownloadImage: " + request.error); }
        }
    }

    void Process(string text)
    {
        links = new List<string>();
        int count = Regex.Matches(text, term).Count;
        int start = 0;
        int end = 0;
        for (int i = 0; i < count; ++i)
        {
            try
            {
                start = text.IndexOf(term, start + 1);
                end = text.IndexOf(mark, start + term.Length);
                string link = text.Substring(start + term.Length, end - start - term.Length);
                if (link.StartsWith(secure))
                {
                    int amp = link.IndexOf(check);
                    if (amp > -1) { link = link.Substring(0, amp); }
                    links.Add(link);
                }
            }
            catch { Debug.Log("[WARN]: Match Error"); continue; }
        }
        if (links.Count == 0) { Debug.Log("[WARN]: No Links Received"); }
        else { Debug.Log("[INFO]: " + links[0]); }
        once = false;
        foreach (string link in links)
        {
            StartCoroutine(DownloadImage(link));
        }
    }
}
