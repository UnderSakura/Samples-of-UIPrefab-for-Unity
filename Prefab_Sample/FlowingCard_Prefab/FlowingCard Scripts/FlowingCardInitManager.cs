using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowingCardInitManager : MonoBehaviour
{
    [Header("请放入存放滑片内容信息的数据")]
    public ContentData contentData;
    [Header("请放入UICard的Prefab")]
    public GameObject UIContent_Prefab;
    [Header("设置生成间距-px")]
    public float InitLength;
    [Header("设置悬浮距离-px")]
    public float DistanceFlow;

    private List<Content> contents;

    private void Start()
    {
        contents = contentData.ContentList;

        InitFlowCardsInList();
    }

    public void InitFlowCardsInList()
    {
        GameObject[] gameObjects = new GameObject[contents.Count];

        var tmp_GameObject = new GameObject("EmptyGameObject");

        tmp_GameObject.transform.position = new Vector3(0, 0, 0);
        var parentObject = tmp_GameObject.transform;

        for (int index = 0; index < contents.Count; index++)
        {
            var current_Trans_x = parentObject.position.x + InitLength;
            parentObject.position = new Vector3(current_Trans_x, parentObject.position.y, parentObject.position.z);
            var thisContent = Instantiate(UIContent_Prefab, parentObject);
            gameObjects[index] = thisContent;
            thisContent.GetComponentInChildren<Text>().text = contents[index].ContentText;
            if (contents[index].FrontImage != null) {
                GameObject.Find("FrontImage").GetComponent<Image>().sprite = contents[index].FrontImage;
            }

            thisContent.transform.SetParent(tmp_GameObject.transform);
            thisContent.transform.localPosition = new Vector3(parentObject.position.x - (GameObject.Find("FC_Canvas").GetComponent<RectTransform>().sizeDelta.x /2f), 
                parentObject.position.y, parentObject.position.z - DistanceFlow);
            Debug.Log(parentObject.position);
        }
        tmp_GameObject.transform.SetParent(GameObject.Find("FC_Canvas").transform);
        tmp_GameObject.transform.localPosition = new Vector3(0, 0, 0);
    }
}
