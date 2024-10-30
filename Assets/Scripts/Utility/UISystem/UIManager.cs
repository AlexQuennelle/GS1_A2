using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Utility.Singleton;
using static PanelBase;

public class UIManager : MonoSingleton<UIManager>
{
    #region Fields

    private Transform rearmost;
    private Transform rear;
    private Transform middle;
    private Transform front;
    private Transform forefront;

    private const string rearmostName = "Rearmost";
    private const string rearName = "Rear";
    private const string middleName = "Middle";
    private const string frontName = "Front";
    private const string forefrontName = "Forefront";

    private const string PanelObjPath = "UI/PanelScript/";

    private Dictionary<string, PanelBase> panelDic = new Dictionary<string, PanelBase>();
    #endregion

    #region Property

    public Transform Rearmost
    {
        get
        {
            if (rearmost == null)
            {
                rearmost = transform.Find(rearmostName);
            }

            return rearmost;
        }
    }

    public Transform Rear
    {
        get
        {
            if (rear == null)
            {
                rear = transform.Find(rearName);
            }

            return rear;
        }
    }

    public Transform Middle
    {
        get
        {
            if (middle == null)
            {
                middle = transform.Find(middleName);
            }

            return middle;
        }
    }

    public Transform Front
    {
        get
        {
            if (front == null)
            {
                front = transform.Find(frontName);
            }

            return front;
        }
    }

    public Transform Forefront
    {
        get
        {
            if (forefront == null)
            {
                forefront = transform.Find(forefrontName);
            }

            return forefront;
        }
    }

    #endregion

    #region LifeCycle

    protected override void Awake()
    {
        base.Awake();

        CreateOverlayCanvas();
        CreateEventSystem();
        ShowPanel<StartPanel>(PanelBase.PanelShowLayer.Front, PanelBase.Ani.None);
        DialogueManager.Instance.Initialize();
    }

    #endregion

    #region Internal Methods
    private void CreateOverlayCanvas()
    {
        if (FindObjectOfType<Canvas>()) return;

        gameObject.layer = LayerMask.NameToLayer("UI");

        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 30000;

        CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        canvasScaler.matchWidthOrHeight = Screen.width > Screen.height ? 1 : 0;

        GraphicRaycaster graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();

        GameObject rearmost = new GameObject(rearmostName);
        rearmost.transform.SetParent(transform, false);

        GameObject rear = new GameObject(rearName);
        rear.transform.SetParent(transform, false);

        GameObject middle = new GameObject(middleName);
        middle.transform.SetParent(transform, false);

        GameObject front = new GameObject(frontName);
        front.transform.SetParent(transform, false);

        GameObject forefront = new GameObject(forefrontName);
        forefront.transform.SetParent(transform, false);
    }

    private void CreateEventSystem()
    {
        if (FindObjectOfType<EventSystem>()) return;

        GameObject eventSystem = new GameObject("EventSystem");

        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }
    #endregion

    #region External Methods
    public T ShowPanel<T>(PanelShowLayer layer = PanelShowLayer.Middle, Ani type = Ani.None, Action<T> onPanelCreated = null) where T : PanelBase
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            T existingPanel = panelDic[panelName] as T;
            onPanelCreated?.Invoke(existingPanel); 
            return existingPanel;
        }

        GameObject panel = Instantiate(Resources.Load<GameObject>(PanelObjPath + panelName));
        Transform parent = null;
        switch (layer)
        {
            case PanelShowLayer.Rearmost:
                parent = Rearmost;
                break;
            case PanelShowLayer.Rear:
                parent = Rear;
                break;
            case PanelShowLayer.Middle:
                parent = Middle;
                break;
            case PanelShowLayer.Front:
                parent = Front;
                break;
            case PanelShowLayer.Forefront:
                parent = Forefront;
                break;
        }

        panel.transform.SetParent(transform, false);
        panel.transform.SetParent(parent);
        T panelT = panel.GetComponent<T>();
        panelDic.Add(panelName, panelT);

        onPanelCreated?.Invoke(panelT);

        switch (type)
        {
            case Ani.None:
                panelT.ShowMe(Ani.None);
                break;
            case Ani.Fade:
                panelT.ShowMe(Ani.Fade);
                break;
        }

        return panelT;
    }

    public void HidePanel<T>(Ani type = Ani.None, Action task = null) where T : PanelBase
    {
        string panelName = typeof(T).Name;

        if (panelDic.ContainsKey(panelName))
        {
            T panelT = panelDic[panelName] as T;

            switch (type)
            {
                case Ani.None:
                    Destroy(panelT.gameObject);
                    panelDic.Remove(panelName);
                    break;
                case Ani.Fade:
                    panelT.HideMe(() =>
                    {
                        Destroy(panelT.gameObject);
                        panelDic.Remove(panelName);
                    });
                    break;
            }

            task?.Invoke();
        }
        else
        {
            Debug.LogErrorFormat("{0} panel error!", panelName);
        }
    }

    public T GetPanel<T>() where T : PanelBase
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        else
        {
            Debug.LogErrorFormat("{0} panel error!", panelName);
            return null;
        }
    }

    public bool IsPanelShowed<T>() where T : PanelBase
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return true;
        return false;
    }

    #endregion
}
