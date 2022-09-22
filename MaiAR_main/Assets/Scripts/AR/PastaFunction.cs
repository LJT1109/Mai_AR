using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class PastaFunction : MonoBehaviour
{

    public Docker Frame;
    public Docker Sticker;
    [System.Serializable]
    public class Docker
    {
        public GameObject UiObject;
        public GameObject Content;
        public GameObject Prefab;
    }

    public GameObject FrameCanvas;

    private void Start()
    {
        if (GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].paste == null)
            GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].paste = new List<Texture2D>();
        if (GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].frame == null)
            GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].frame = new List<GlobalSet.Frame>();
        createui();
    }

    public void ClickFrame()
    {
        Frame.UiObject.SetActive(true);
    }
    public void ClickSticker()
    {
        Sticker.UiObject.SetActive(true);
    }
    public void ClickBack()
    {
        Sticker.UiObject.SetActive(false);
        Frame.UiObject.SetActive(false);
    }

    public void createui()
    {
        if (GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].paste.Count != 0)
        {
            foreach (var item in GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].paste)
            {
                GameObject sticker = Instantiate(Sticker.Prefab, Sticker.Content.transform);
                sticker.GetComponent<Image>().sprite = Sprite.Create(item, new Rect(0.0f, 0.0f, item.width, item.height), new Vector2(0.5f, 0.5f), 100.0f);
                sticker.GetComponent<Sticker>().texture = item;
            }
        }



        if (GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].frame.Count != 0)
        {
            print(GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].frame.Count);
            foreach (var item in GlobalSet.Keeptopics[GlobalSet.CurrentTopicID].frame)
            {
                GameObject frame = Instantiate(Frame.Prefab, Frame.Content.transform);
                frame.GetComponent<Image>().sprite = Sprite.Create(item.icon, new Rect(0.0f, 0.0f, item.icon.width, item.icon.height), new Vector2(0.5f, 0.5f), 100.0f);
                frame.GetComponent<Frame>().texture = item.screen;
            }
        }
    }

    public void clear()
    {
        for (int i = 0; i < FrameCanvas.transform.childCount; i++)
        {
            Destroy(FrameCanvas.transform.GetChild(0).gameObject);
        }
    }

}
