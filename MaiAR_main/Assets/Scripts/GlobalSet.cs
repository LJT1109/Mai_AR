using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GlobalSet : MonoBehaviour
{
    [Serializable]
    public class Topics 
    {
        public string wwwFolderName;
        public int id;
        public string name;
        public Texture2D icon;
        public int version;
        public List<TrackObject> ar;
        public List<Texture2D> paste;
        public List<Frame> frame;
    }
    public static bool IsOpenFram;

    [Serializable]
    public class Frame
    {
        public Texture2D icon;
        public Texture2D screen;
    }

    public static int CurrentTopicID =-1;
    [Serializable]
    public class TrackObject 
    {
        public Material objectMaterial;
        public GameObject objectModel;
    }


    public static List<Topics> Keeptopics = new List<Topics>();
    public List<Topics> topics_Preview;
    public static string KeepserverIP;
    public string serverIP_Preview;

    private void Start()
    {

        //if (GlobalSet.CurrentTopic != null) print(GlobalSet.CurrentTopic.name);
        if (KeepserverIP == null && serverIP_Preview != null)
        {
            KeepserverIP = serverIP_Preview;
            //print("KeepserverIP = serverIP_Preview");
        }
        if (KeepserverIP != null && serverIP_Preview == null)
        {
            serverIP_Preview = KeepserverIP;
            //print(Keeptopics.Count);
            topics_Preview = Keeptopics;
            //print("serverIP_Preview = KeepserverIP;");
        }

        
    }

    private void Update()
    {
        topics_Preview = Keeptopics;
    }



    public static int SelectIndexByID(int id)
    {
        foreach (var item in Keeptopics)
        {
            if(id == item.id)
            {
                return Keeptopics.IndexOf(item);
            }
        }
        return -1;

        
    }
}
