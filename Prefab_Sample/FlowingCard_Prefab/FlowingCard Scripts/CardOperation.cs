using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CardOperation : MonoBehaviour
{
    public RectTransform CardFront;
    public RectTransform CardBack;
    public Transform targetFacePoint;

    public Collider col;

    private bool showingBack = false;

    public Button BackFaceButton;
    public Button FrontFaceButton;

    private void Start()
    {
        //背牌处的Button
        BackFaceButton.GetComponent<Button>();
        BackFaceButton.onClick.AddListener(BackFaceOnClick);

        //牌面的Button
        FrontFaceButton.GetComponent<Button>();
        FrontFaceButton.onClick.AddListener(FrontFaceOnClick);
    }


    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(origin: Camera.main.transform.position,
                                  direction: (-Camera.main.transform.position + targetFacePoint.position).normalized,
                                  maxDistance: /*(-Camera.main.transform.position + targetFacePoint.position).magnitude*/1000f);
        bool passedThroughColiderOnCard = false;
        foreach (RaycastHit h in hits)
        {
            if (h.collider == col)
                passedThroughColiderOnCard = true;
        }
        if (passedThroughColiderOnCard != showingBack)
        {
            showingBack = passedThroughColiderOnCard;
            if (showingBack)
            {
                CardFront.gameObject.SetActive(false);
                CardBack.gameObject.SetActive(true);
            }
            else
            {
                CardFront.gameObject.SetActive(true);
                CardBack.gameObject.SetActive(false);
            }
        }
    }
    public void BackFaceOnClick()
    {
        Animator ThisAtor = this.GetComponent<Animator>();
        ThisAtor.SetTrigger("ToFrontTrigger");

        Debug.Log("Back");
    }

    public void FrontFaceOnClick()
    {
        Animator ThisAtor = this.GetComponent<Animator>();
        ThisAtor.SetTrigger("ToBackTrigger");

        Debug.Log("Front");
    }
}
