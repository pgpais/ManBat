using Assets.Scripts.Lighting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightMask : MonoBehaviour {

    public Transform LightTranformContainer;
    public SpriteRenderer LightSpriteRenderer;

    [Header("Func Stuff")]
    public float aFunctions =8f;
    public float aFadeRatio =16f;
    public float bLog = 1f;
    public float addLog = 16f;
    public float addExp =1f;

    public float XFactorFade = 1f;
    public float XFactorExpand = 7f;



    [SerializeField]
    [Range (0,2)]
    private float _lightPower;
    public float LightPower
    {
        get
        {
            return _lightPower;
        }
        set
        {
            _lightPower = value;
        }
    }


    [Range(0f, 1f)]
    [Tooltip("Set the light intensity. Implementation wise this is done by variating the alpha.")]
    [SerializeField]
    private float _lightIntensity;
    public float LightIntensity
    {
        get
        {
            return _lightIntensity;
        }
        set
        {
            _lightIntensity = value;
            UpdateIntensity();
        }
    }

    
    [Tooltip("Set the light radius.")]
    [SerializeField]
    private float _radius;
    public float Radius
    {
        get
        {
            return _radius;
        }
        set
        {
            _radius = value;
            UpdateRadius();
        }
    }


    [Tooltip("Set whether the light should suicide.")]
    public bool HasLifetime;

    [Tooltip("Set the amount of time the light takes to suicide.")]
    public float LifeTime;

    [Tooltip("Should the light fade while killing itself.")]
    public bool Fades;

    public bool Expands = false;
    public float ExpandTime = 1.5f;
    public float ExpandRadius = 10f;

    public bool FadeAfterExpand = false;

    private bool _fading = false;
    private bool _expanding = false;

    void OnDisable()
    {
        if (Application.isPlaying)
        {
            Fades = false;
            Expands = false;
            FadeAfterExpand = false;
            Radius = 0;
            LightIntensity = 1;
            _fading = false;
            _expanding = false;
        }
    }
    

	void Update () {
        UpdateIntensity();

        if (HasLifetime && Application.isPlaying)
            StartCoroutine(LifeRoutine());

        if (Fades && !_fading && Application.isPlaying)
            StartCoroutine(FFade());

        if (Expands && !_expanding && Application.isPlaying)
            StartCoroutine(FExpand());

#if UNITY_EDITOR
        if (!Application.isPlaying)
            UpdateRadius();
#endif
    }

    private void UpdateRadius()
    {
        var newScale = transform.localScale;
        newScale.x = Radius;
        newScale.y = Radius;
        transform.localScale = newScale;
    }

    private void UpdateIntensity()
    {
        var newColor = LightSpriteRenderer.color;
        var invertedValue = 1 - LightIntensity;
        newColor.a = Mathf.Clamp01(invertedValue);
        LightSpriteRenderer.color = newColor;
        LightSpriteRenderer.sortingOrder = Mathf.FloorToInt( invertedValue * 255);
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(LifeTime);
        LightManager.Instance.DestroyLight(this);
    }


    private IEnumerator FFade()
    {
        var startingIntensity = LightIntensity;
        var startTime = Time.time;
        var maxTime = (LightPower * XFactorFade);

        _fading = true;
        while (LightIntensity > 0)
        {
            var timePassed = Time.time - startTime;
            /*
            timePassed = Mathf.Max(timePassed - LightPower * XFactorFade, 0);
            LightIntensity = QuadraticFunction(timePassed, aFunctions, bExponential, addExp);
            
            */

            var timeStep = timePassed / maxTime;
            LightIntensity = Mathf.Clamp(LogFunction(timeStep, aFadeRatio), 0f, 1f);



            yield return null;
        }

        LightManager.Instance.DestroyLight(this);
    }

    public IEnumerator FExpand()
    {
        var startingRadius = InverseLogFunction(0, aFunctions);
        var startTime = Time.time;

        while (true)
        {
            var timePassed = Time.time - startTime;
            Radius = InverseLogFunction(timePassed, aFunctions) * XFactorExpand * LightPower; ;

            yield return null;
        }
    }

    private float QuadraticFunction (float x, float a, float b, float maxIntensity)
    {
        //-(2)^.5x + 20 
        return (-Mathf.Pow(x,b)) + maxIntensity;
    }

    private float InverseLogFunction(float x, float a)
    {
        //5log(x) 
        return (-1/ (Mathf.Pow(x, a) +1)) +1 ;
    }

    private float LogFunction(float x, float a)
    {
        return -Mathf.Log(x, a);
    }


    private float LinearFunction (float x, float max)
    {
        return x / max;
    }

    
}
