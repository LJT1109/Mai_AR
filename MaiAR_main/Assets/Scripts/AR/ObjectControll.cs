using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControll : MonoBehaviour
{
    public GameObject game;
    public void Destory()
    {
        game = this.gameObject;
        Destroy(game);
    }

    private void Awake()
    {
        deselect();
    }
    public void select()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void deselect()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
