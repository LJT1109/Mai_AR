using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sticker : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject canvas;
    public Texture2D texture;
    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("StickerCanvas");
    }
    public void creatSticker()
    {
        GameObject game =  Instantiate(Prefab, canvas.transform);
        
        game.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
