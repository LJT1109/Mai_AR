using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;


namespace myFtpDlHelper
{
    public class CoroutineWithData : MonoBehaviour
    {
        public Coroutine coroutine { get; private set; }
        public object result;
        private IEnumerator target;


        public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
        {
            this.target = target;
            this.coroutine = owner.StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            while (target.MoveNext())
            {
                result = target.Current;
                yield return result;
            }
        }

        public static IEnumerator DownloadFile(string path, string localPath)
        {
            string uri = Path.Combine(GlobalSet.KeepserverIP, "res", path);
            if (!File.Exists(localPath)) 
            { 
                using (UnityWebRequest www = UnityWebRequest.Get(uri))
                {
                    yield return www.Send();
                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        print(localPath);
                        System.IO.File.WriteAllBytes(localPath, www.downloadHandler.data);
                    }
                }
            }
        }

        public static IEnumerator GetwwwDir(string path = "")
        {

            string uri = GlobalSet.KeepserverIP + "getdir.php?path=" + path;
            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.SendWebRequest();
            //print(uri);
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
                yield break;
            }
            yield return request.downloadHandler.text;
        }

        private void Start()
        {

            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "res")))
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "res"));
        }
    }



}


