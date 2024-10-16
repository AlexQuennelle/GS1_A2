using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(CanvasGroup))]
public abstract class PanelBase : MonoBehaviour
{
    public enum Ani
    {
        None,
        Fade 
    }

    public enum PanelShowLayer
    {
        Rearmost, 
        Rear, 
        Middle, 
        Front, 
        Forefront
    }

    #region Fields
    [SerializeField, Header("Fade speed")]
    protected float alphaSpeed = 2f;

    private CanvasGroup _canvasGroup;
    private bool _isShow;
    private Action _action;

    private Ani ani;

    private Dictionary<string, Component> cacheCompents = new Dictionary<string, Component>();
    #endregion

    #region LifeCycle
    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        ShowOrHideAni();
    }

    protected virtual void OnDisable()
    {
        cacheCompents.Clear();
    }
    #endregion

    #region Methods
    protected abstract void Init();

    public void ShowMe(Ani ani = Ani.None)
    {
        _canvasGroup.alpha = 0f;
        _isShow = true;
        this.ani = ani;
    }

    public void HideMe(Action task)
    {
        _canvasGroup.alpha = 1f;
        _isShow = false;
        if (task != null)
            _action = task;
    }

    private void ShowOrHideAni()
    {
        if (_isShow && _canvasGroup.alpha < 1f)
        {
            switch (ani)
            {
                case Ani.None:
                    _canvasGroup.alpha = 1f;
                    break;
                case Ani.Fade:
                    _canvasGroup.alpha = Mathf.Clamp(_canvasGroup.alpha + (Time.deltaTime * alphaSpeed), 0f, 1f);
                    break;
            }
        }
        else if (!_isShow && _canvasGroup.alpha > 0f)
        {
            _canvasGroup.alpha = Mathf.Clamp(_canvasGroup.alpha - (Time.deltaTime * alphaSpeed), 0f, 1f);
            if (_canvasGroup.alpha <= 0f)
                _action?.Invoke();
        }
    }

    protected T GetController<T>(string childObjectName, Transform parent = null) where T : Component
    {
        parent = parent ?? this.transform;
        if (cacheCompents.ContainsKey(childObjectName))
            return cacheCompents[childObjectName] as T;

        Transform childTransform = parent.Find(childObjectName);

        if (childTransform != null)
        {
            T component = childTransform.GetComponent<T>();
            if (component != null)
            {
                if (!cacheCompents.ContainsKey(childObjectName))
                {
                    cacheCompents.Add(childObjectName, component);
                }
                return component;
            }
        }

        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (parent.GetChild(i) == null) continue;

            T component = GetController<T>(childObjectName, parent.GetChild(i));
            if (component != null)
            {
                if (!cacheCompents.ContainsKey(childObjectName))
                {
                    cacheCompents.Add(childObjectName, component);
                }
                return component;
            }
        }
        return null;
    }

    #endregion
}