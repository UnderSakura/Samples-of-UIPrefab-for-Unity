using System.Collections;
using System.Collections.Generic;
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

    void InitCardsInList() {
        GameObject[] gameObjects = new GameObject[contents.Count];

        var tmp_GameObject = new GameObject("EmptyGameObject");
        
        var parentObject = tmp_GameObject.transform;

        for (int index = 0; index < contents.Count; index++) {
            
            var current_Trans_x = parentObject.transform.position.x + InitLength;
            parentObject.transform.position = new Vector3(current_Trans_x, parentObject.transform.position.y, parentObject.transform.position.z);
            var thisContent = Instantiate(UIContent_Prefab, parentObject.transform);
            gameObjects[index] = thisContent;
            thisContent.GetComponentInChildren<Text>().text = contents[index].ContentText;

            thisContent.transform.SetParent(tmp_GameObject.transform);
        }
        tmp_GameObject.transform.SetParent(GameObject.Find("Canvas").transform);

        var rectTrans = tmp_GameObject.AddComponent<RectTransform>();
        var SetContent = this.GetComponentInChildren<ScrollRect>();
        rectTrans.anchorMin = gameObjects[0].GetComponent<RectTransform>().anchorMin;
        rectTrans.anchorMax = gameObjects[contents.Count - 1].GetComponent<RectTransform>().anchorMax;
        rectTrans.pivot.Equals(gameObjects[0].GetComponent<RectTransform>().pivot);
        SetContent.content = tmp_GameObject.GetComponent<RectTransform>();
    }
}
