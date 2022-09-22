using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicBuilder : MonoBehaviour
{
    public GameObject TopicBase, TopicEmpty;
    public Transform Content;
    public Vector2 Offset = new Vector2(75f, -64f);
    public int count = 0,emptyCount = 3;
    public void CreateButton()
    {
        //GlobalSet.Keeptopics.Sort();
        
        foreach (var item in GlobalSet.Keeptopics)
        {
            GameObject newTopic;
            newTopic= Instantiate(TopicBase, Content);
            TopicSet topicSet = newTopic.GetComponent<TopicSet>();
            if(topicSet.topicBg.material.mainTexture != null)
            topicSet.topicBg.material.mainTexture = item.icon;
            topicSet.topicName.text = item.name;
            topicSet.id = item.id;
            topicSet.GetComponent<RectTransform>().anchoredPosition += new Vector2(((int)(count / 2)) * Offset.x, (count % 2) * Offset.y);

            count++;
        }

        CreateEmptyButton();

    }

    public void CreateEmptyButton()
    {
        //GlobalSet.Keeptopics.Sort();

        for (int i = 0; i < emptyCount; i++)
        {
            GameObject newTopic;
            newTopic = Instantiate(TopicEmpty, Content);
            
            newTopic.GetComponent<RectTransform>().anchoredPosition += new Vector2((count % 2) * Offset.x, ((int)(count / 2)) * Offset.y);

            count++;
        }


        Content.GetComponent<RectTransform>().sizeDelta +=  new Vector2(0, (int)(count / 2) * Mathf.Abs( Offset.y));
    }
}
