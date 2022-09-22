using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject canvas;
    public Texture2D texture;
    private void Awake()
    {
        GlobalSet.IsOpenFram = false;
        canvas = GameObject.FindGameObjectWithTag("Frame");
    }
    public void creatSticker()
    {
        if (canvas.transform.childCount > 0)
        {
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                Destroy(canvas.transform.GetChild(0).gameObject);
            }

        }
        GameObject game = Instantiate(Prefab, canvas.transform);
        GlobalSet.IsOpenFram = true;
        game.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }


}
