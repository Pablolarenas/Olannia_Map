using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [SerializeField] private TextAsset enfermedadMaxima;
    private List<InstanceContent> listOfContent;
    
	void Awake ()
    {
        listOfContent = new List<InstanceContent>();
        JSONNode data = JSON.Parse(enfermedadMaxima.ToString());

        for (int city = 0; city < data.Count; city++)
        {
            Debug.Log(data[city]["nombre"].Value);
            listOfContent.Add(new InstanceContent(data[city]["nombre"].Value, data[city]["imagen"].Value, data[city]["titulo"].Value, data[city]["descripcion"].Value));
        }
    }
	
	void Update ()
    {
		
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
    public string Title;
    public string Description;

    public InstanceContent(string name, string image, string title, string description)
    {
        Name = name;
        Image = image;
        Title = title;
        Description = description;
    }
}
