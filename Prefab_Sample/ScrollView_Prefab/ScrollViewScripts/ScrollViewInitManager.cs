using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewInitManager : MonoBehaviour
{
    [Header("请放入存放滑片内容信息的数据")]
    public ScrollViewContentData contentData;
    [Header("请放入UICard的Prefab")]
    public GameObject UIContent_Prefab;
    [Header("设置生成间距-px")]
    public float InitLength;

    private RectTransform SV_ContentTrans;
    private List<Content> contents;

    private void Start() {
        SV_ContentTrans = GameObject.Find("Content").GetComponent<RectTransform>();
        contents = contentData.ContentList;

        InitCardsInList();
    }

    /**
     * 点击Button生成滑片，请调用这个方法
     */
    public void InitCardsInList() {
        GameObject[] gameObjects = new GameObject[contents.Count];

        var tmp_GameObject = new GameObject("EmptyGameObject");
        tmp_GameObject.transform.position = new Vector3(0, GameObject.Find("SV_Canvas").GetComponent<RectTransform>().sizeDelta.y, 0);
        tmp_GameObject.transform.SetParent(GameObject.Find("SV_Canvas").transform);
        var parentObject = tmp_GameObject.AddComponent<RectTransform>();
        parentObject.anchoredPosition = Vector2.zero;
        tmp_GameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((InitLength * contents.Count + InitLength / 4), 600f);

        for (int index = 0; index < contents.Count; index++) {
            //Debug.Log(parentObject.anchoredPosition.x);
            if (index == 0) { parentObject.anchoredPosition = InitCurrentCardInWorld(parentObject, index, gameObjects, true, tmp_GameObject); }
            else {
                parentObject.anchoredPosition = InitCurrentCardInWorld(parentObject, index, gameObjects, false, tmp_GameObject);
            } 
        }

        var SetContent = this.GetComponentInChildren<ScrollRect>();
        SetContent.content = tmp_GameObject.GetComponent<RectTransform>();

        //Debug.Log(tmp_GameObject.GetComponent<RectTransform>().sizeDelta.x / 2f);
    }

    Vector2 InitCurrentCardInWorld(RectTransform parentObject, int index, GameObject[] gameObjects, bool isIndexO, GameObject parentGameObject) {
        float current_Trans_x = InitLength / 2f;
        if (!isIndexO) {
            current_Trans_x = parentObject.anchoredPosition.x + InitLength;
        }
        parentObject.anchoredPosition = new Vector2(current_Trans_x, parentObject.anchoredPosition.y);
        var thisContent = Instantiate(UIContent_Prefab);
        gameObjects[index] = thisContent;
        thisContent.GetComponentInChildren<Text>().text = contents[index].ContentText;
        var tmp_rectTransform = thisContent.GetComponent<RectTransform>();
        
        tmp_rectTransform.gameObject.transform.SetParent(GameObject.Find("EmptyGameObject").transform);
        tmp_rectTransform.anchoredPosition = new Vector2((parentObject.anchoredPosition.x - (parentGameObject.GetComponent<RectTransform>().sizeDelta.x / 2f)), parentObject.anchoredPosition.y);
        Debug.Log("减下的固定长度父物体长度的一半：" + (parentGameObject.GetComponent<RectTransform>().sizeDelta.x / 2f));
        Debug.Log((parentObject.anchoredPosition.x - (parentGameObject.GetComponent<RectTransform>().sizeDelta.x / 2f)));

        return parentObject.anchoredPosition;
    }
}
