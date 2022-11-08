using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CardOperation : MonoBehaviour
{
    public RectTransform CardFront;

    public RectTransform CardBack;

    public GameObject OnChooseCard;
    private Button ReButton;
    
    private int Speed = 30;
    [SerializeField] bool isMoveToGoal;
    
    [SerializeField]
    private Vector3 DispCardPoint;
    private Vector3 Temp_Position;


    public Transform targetFacePoint;

    public Collider col;

    private bool showingBack = false;

    public Button BackFaceButton;
    public Button FrontFaceButton;

    private void Start()
    {
        ReButton = GameObject.Find("ReButton").GetComponent<Button>();
        ReButton.onClick.AddListener(DestroyThis);
        //背牌处的Button
        BackFaceButton.GetComponent<Button>();
        BackFaceButton.onClick.AddListener(BackFaceOnClick);

        //牌面的Button
        FrontFaceButton.GetComponent<Button>();
        FrontFaceButton.onClick.AddListener(FrontFaceOnClick);

        DispCardPoint = GameObject.Find("DispCardPoint").GetComponent<Transform>().position;
    }


    void Update()
    {
        //相机发射射线检测碰撞
        RaycastHit[] hits;
        hits = Physics.RaycastAll(origin: Camera.main.transform.position,
                                  direction: (-Camera.main.transform.position + targetFacePoint.position).normalized,
                                  maxDistance: (-Camera.main.transform.position + targetFacePoint.position).magnitude);
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
                OnChooseCard.SetActive(false);
            }
            else
            {
                CardFront.gameObject.SetActive(true);
                CardBack.gameObject.SetActive(false);
                OnChooseCard.SetActive(true);
            }
        }

        if (isMoveToGoal)
        {
            //Goal = DispCardPoint.transform.position;
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position,
                DispCardPoint, Time.deltaTime * Speed);
            if (this.gameObject.transform.position == DispCardPoint)
                isMoveToGoal = false;
        }
        isOtherPointOnClick();
    }
    public void BackFaceOnClick()
    {
        Animator ThisAtor = this.gameObject.GetComponent<Animator>();
        ThisAtor.SetTrigger("ClickTrigger");
        //直接瞬移的方法
        Temp_Position= this.gameObject.transform.position;
        //this.gameObject.transform.position = DispCardPoint.transform.position;
        isMoveToGoal = true;

    }

    public void FrontFaceOnClick()
    {
        Animator ThisAtor = this.gameObject.GetComponent<Animator>();
        ThisAtor.SetTrigger("ClickTrigger2");
        //瞬移
        this.gameObject.transform.position = Temp_Position;
        isMoveToGoal = false;
    }

    public void isOtherPointOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //print("MouseDown");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (!showingBack && hitInfo.collider.gameObject.CompareTag("Card"))
                {
                    FrontFaceOnClick();
                }
            }
        }
    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
