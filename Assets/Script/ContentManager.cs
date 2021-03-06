﻿using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [SerializeField] private TextAsset enfermedadMaxima;
    [SerializeField] private TextAsset enfermedadMaximaTimeline;
    private List<InstanceContent> listOfContent;
    public List<InstanceContent> ListOfContentTimeline;

    void Awake ()
    {
        listOfContent = new List<InstanceContent>();
        JSONNode data = JSON.Parse(enfermedadMaxima.ToString());

        for (int city = 0; city < data.Count; city++)
        {
            Debug.Log(data[city]["nombre"].Value);
            listOfContent.Add(new InstanceContent(data[city]["nombre"].Value, data[city]["imagen"].Value, data[city]["titulo"].Value, data[city]["descripcion"].Value));
        }

        ListOfContentTimeline = new List<InstanceContent>();
        data = JSON.Parse(enfermedadMaximaTimeline.ToString());

        for (int city = 0; city < data.Count; city++)
        {
            ListOfContentTimeline.Add(new InstanceContent(data[city]["nombre"].Value, data[city]["imagen"].Value, data[city]["imagen2"].Value, data[city]["titulo"].Value, data[city]["descripcion"].Value));
        }
    }


    public InstanceContent GetContentInstance(string name)
    {
        return listOfContent.Where(instance => instance.Name == name).SingleOrDefault();
    }

}

public class InstanceContent
{
    public string Name;
    public string Image;
    public string Image2;
    public string Title;
    public string Description;

    public InstanceContent(string name, string image, string title, string description)
    {
        Name = name;
        Image = image;
        Title = title;
        Description = description;
    }

    public InstanceContent(string name, string image, string image2, string title, string description)
    {
        Name = name;
        Image = image;
        Image2 = image2;
        Title = title;
        Description = description;
    }
}
