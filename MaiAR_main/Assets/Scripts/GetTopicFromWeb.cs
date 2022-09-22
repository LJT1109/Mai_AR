using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using myFtpDlHelper;
[RequireComponent(typeof(CoroutineWithData))]
public class GetTopicFromWeb : MonoBehaviour
{
    
    private void Start()
    {
        
        StartCoroutine(GetTopics());
    }
    
    
    public IEnumerator GetTopics()
         {
        yield return new WaitForSeconds(1);
        string path = "";
        
        CoroutineWithData cd = new CoroutineWithData(this, CoroutineWithData.GetwwwDir(path));
        yield return cd.coroutine;
        //print(cd.result);

        string[] getTopics = cd.result.ToString().Split('@');

        foreach (var item in getTopics)
        {
            if (item == "") continue;
            //print(item.Split('.')[1]); ;s
            GlobalSet.Topics topics = new GlobalSet.Topics();
            topics.name = item.Split('.')[1];
            //print(item.Split('.')[0]);
            topics.id = int.Parse(item.Split('.')[0]);
            topics.wwwFolderName = item;
            //1.Create Folder
            string folderpath = Path.Combine(Application.persistentDataPath, "res", topics.name);
            CreateFolder(folderpath);
            //print(folderpath);

            //1-5.GetVersion
            path = item;
            //print(path);
            cd = new CoroutineWithData(this, CoroutineWithData.GetwwwDir(path));
            yield return cd.coroutine;
            //print(cd.result);
            string[] sub1 = cd.result.ToString().Split('@');

            foreach (var item2 in sub1)
            {
                
                if (item2 == "icon.png")
                {
                    yield return StartCoroutine(CoroutineWithData.DownloadFile(Path.Combine(item.ToString(), "icon.png"), Path.Combine(folderpath, "icon.png")));
                    topics.icon = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                    topics.icon.LoadImage(File.ReadAllBytes(Path.Combine(folderpath, "icon.png")));
                }
                else if (item2.Length > 0 )
                {
                    if(item2.ToUpper()[0] == 'V')
                    {
                        topics.version = int.Parse( item2.ToUpper().Split('V')[1]);
                        int localVersion = LocalVersion(folderpath);
                        print(string.Format("{0}-{1}", localVersion.ToString(), topics.version.ToString()));
                        if (localVersion >0 && localVersion < topics.version)
                        {
                            //print(Path.Combine(folderpath, "V" + localVersion.ToString()));
                            for (int i = 0; i < topics.version; i++)
                            {
                                
                                if(Directory.Exists(Path.Combine(folderpath, "V" + i.ToString())))
                                {
                                    print("Delect " + Path.Combine(folderpath, "V" + i.ToString()));
                                    Directory.Delete(Path.Combine(folderpath, "V" + i.ToString()), true);
                                    if (Directory.Exists(Path.Combine(folderpath, "V" + i.ToString())))
                                        Directory.Delete(Path.Combine(folderpath, "V" + i.ToString()));
                                }
                                    
                            }
                        }
                        if (localVersion != topics.version)
                        {
                            print(folderpath);
                            CreateFolder(Path.Combine(folderpath, "V" + topics.version.ToString()));
                        }
                    }
                }

                
            }
            
            GlobalSet.Keeptopics.Add(topics);
        }
        FindObjectOfType<TopicBuilder>().CreateButton();

    }

    int LocalVersion(string path)
    {
        
        string[] folder = Directory.GetDirectories(path);
        foreach (var item in folder)
        {
            string[] itemPath = item.Split('\\');
            //print(item);
            if (itemPath[itemPath.Length - 1][0] == 'V')
            {
                
                return int.Parse(itemPath[itemPath.Length - 1].Split('V')[1]);
                break;
            }
        }
        return -1;
    }

    void CreateFolder(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
    /*
    IEnumerator DownloadFile(string path,string localPath)
    {
        string uri = Path.Combine( GlobalSet.KeepserverIP , "res" , path);
        
        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                System.IO.File.WriteAllBytes(localPath, www.downloadHandler.data);

            }
        }

    }

    */
    
}
