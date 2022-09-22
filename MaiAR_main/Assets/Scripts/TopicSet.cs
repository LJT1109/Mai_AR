using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TopicSet : MonoBehaviour
{
    public Image topicBg;
    public TextMeshProUGUI topicName;
    public int id;
    public GameObject dlBut;
    public void GoAR()
    {
        GlobalSet.CurrentTopicID =  GlobalSet.SelectIndexByID(id);
        
        //print(GlobalSet.CurrentTopic.name);
        SceneManager.LoadScene("Downloading");
    }
}
