using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using myFtpDlHelper;
using TMPro;
[RequireComponent(typeof(CoroutineWithData))]

public class DownloadAsset : MonoBehaviour
{
    public TextMeshProUGUI persent;
    int downloadCount =1, downloaded=1;
    //public GameObject sampleOBJ;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("text", 0, 0.2f);
        StartCoroutine(GetAsset());
    }
    string dot = "";
    private void Update()
    {
        ////persent.text = ((int)(downloaded / downloadCount+1)).ToString() + "%";
        
    }

    void text()
    {
        
        if (dot == "....") dot = "";
        dot += ".";
        persent.text = "downloading" + dot;
        
    }

    IEnumerator GetAsset()
    {
        //;
        string[] pathFrom = new string[]
        {
            GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].wwwFolderName,
            "V"+GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].version,
        };
        string path = Path.Combine(pathFrom);
        path = path.Replace("\\", "/");
        //print(path);
        CoroutineWithData cd = new CoroutineWithData(this, CoroutineWithData.GetwwwDir(path));
        yield return cd.coroutine;
        //print(cd.result);
        string[] localPathFrom = new string[]
        {
            Application.persistentDataPath,
            "res",
            GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].name,
            "V"+GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].version.ToString(),
        };
        
        yield return StartCoroutine( spiltFolderAndFile(cd.result.ToString().Split('@'),Path.Combine(localPathFrom), path));
        print("Download Finish");
        //persent.text = "Download Finish";
        DownloadFinsh();
    }



    IEnumerator spiltFolderAndFile(string[] source,string path,string url)
    {
        
        List<string>[] foldersAndFIles = new List<string>[2];
        foreach (var item in source)
        {

            if (item == "") continue;
            string newurl = url + "/" + item;
            string newpath = path + "/" + item;
            //print(string.Format("{0}\n{1}\n{2}", item, newpath, newurl));
            if (item == "") continue;
            if (item.Contains("."))
            {
                downloadCount++;
                yield return StartCoroutine( CoroutineWithData.DownloadFile(newurl, newpath));
                downloaded++;
                //print(string.Format("{0}\n{1}\n{2}", item, path, url));
            }
            else
            {
                yield return null;
                CoroutineWithData cd = new CoroutineWithData(this, CoroutineWithData.GetwwwDir(newurl));
                yield return cd.coroutine;
                //print(newpath);
                Directory.CreateDirectory(newpath);
                yield return StartCoroutine(spiltFolderAndFile(cd.result.ToString().Split('@'), newpath, newurl));
            }
        }
    }

    void DownloadFinsh()
    {
        LoadAsset();
        //UnityEngine.SceneManagement.SceneManager.LoadScene("AR");
    }

    void LoadAsset()
    {
        string[] folders = new string[]
        {
            Application.persistentDataPath,
            "res",
            GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].name,
            "V" + GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].version
        };
        string[] sub = new string[]
        {
            "ar",
            "frame",
            "paste"
        };
        //persent.text = ("14");

        foreach (var item in sub)
        {
            string path =  Path.Combine(Path.Combine(folders), item);
            //print(path);
            //persent.text = ("13");
            if (item == "ar")
            {
                if (GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].ar == null)
                    GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].ar = new List<GlobalSet.TrackObject>();
                //persent.text = ("12");
                foreach (var item2 in Directory.GetDirectories(path))
                {
                    //print(item2);
                    GlobalSet.TrackObject arobj = new GlobalSet.TrackObject();
                    Texture2D texture = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                    texture.LoadImage(File.ReadAllBytes(Path.Combine(item2, "material.png")));
                    //persent.text = ("11");
                    //print(Path.Combine(item2, "material.png"));
                    //arobj.objectMaterial = new Material(Shader.Find("Unlit/Texture"));
                    //arobj.objectMaterial.mainTexture = texture;
                    //print(File.Exists(Path.Combine(item2, "model.fbx")));

                    //var obj =File.ReadAllBytes(Path.Combine(item2, "model.fbx"));
                    //var obj = Resources.Load(Path.Combine(item2, "model.prefab"));
                    //print(obj.GetType());


                    //arobj.objectModel = Instantiate(sampleOBJ);
                    //arobj.objectModel.GetComponent<MeshFilter>().mesh = obj; 
                    
                    GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].ar.Add(arobj);
                    //persent.text = .text = ("10");
                }
                //print(GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].ar.Count);
                //persent.text = ("9");
            }
            else if(item == "frame")
            {
                //print("item");
                //print(path);
                print(Directory.GetDirectories(path));
                foreach (var item2 in Directory.GetDirectories(path))
                {
                    //persent.text = ("8");
                    //print(item2);
                    string path2 = Path.Combine(path, item2);
                    print(path2);
                    
                    GlobalSet.Frame texture = new GlobalSet.Frame();
                    texture.icon = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                    texture.screen = new Texture2D(4, 4, TextureFormat.RGBA32, false);

                    //persent.text = ("7");
                    string framePath = Path.Combine(path2, "frame.png");
                    texture.screen.LoadImage(File.ReadAllBytes(framePath));
                    string iconPath = Path.Combine(path2, "icon.png");
                    texture.icon.LoadImage(File.ReadAllBytes(iconPath));
                    
                    if (GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].frame == null)
                        GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].frame = new List<GlobalSet.Frame>();
                    GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].frame.Add(texture);
                    //persent.text = ("6");
                }
                //persent.text = ("5");
            }
            else if (item == "paste")
            {

                foreach (var item2 in Directory.GetFiles(path))
                {
                    Texture2D texture = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                    //print(item2);
                    texture.LoadImage(File.ReadAllBytes(item2));

                    if (GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].paste == null)
                        GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].paste = new List<Texture2D>();
                    GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].paste.Add(texture);
                    //persent.text = ("4");
                }
                //persent.text = ("3");
            }
            //persent.text = ("2");
        }
        //persent.text = ("1");
        FindObjectOfType<GlobalSet>().GetComponent<GlobalSet>().topics_Preview = GlobalSet.Keeptopics;
        //persent.text = "Finish";
        UnityEngine.SceneManagement.SceneManager.LoadScene("AR");

    }
}
