using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class UIWobblePop : MonoBehaviour
{
    [Header("Time")]
    public bool useUnscaledTime = true;

    [Header("Wobble")]
    public bool wobbleRotation = true;
    [Range(0f, 15f)] public float rotAmplitude = 2.5f;   // degrees
    [Range(0.1f, 10f)] public float rotFrequency = 1.2f;

    public bool wobbleScale = true;
    [Range(0f, 0.25f)] public float scaleAmplitude = 0.02f; // 2% size wobble
    [Range(0.1f, 10f)] public float scaleFrequency = 1.3f;

    [Header("Pop")]
    public bool autoPop = true;
    public Vector2 popIntervalRange = new Vector2(3f, 7f);   // random seconds between pops
    [Range(0f, 0.6f)] public float popScaleAdd = 0.18f;      // +18% size on pop
    [Range(0.05f, 1f)] public float popDuration = 0.20f;     // total up+down time

    RectTransform rt;
    Vector3 baseScale;
    float baseRotZ;

    float rotPhase, scalePhase;
    float popT;                // 0..1 additive factor controlled by coroutine
    bool isPopping;

    Coroutine popLoopCo, popCo;

    void Awake()
    {
        rt = transform as RectTransform;
        baseScale = rt.localScale;
        baseRotZ = rt.localEulerAngles.z;
        rotPhase = Random.value * 100f; // desync instances
        scalePhase = Random.value * 100f;
    }

    void OnEnable()
    {
        if (autoPop && popLoopCo == null)
            popLoopCo = StartCoroutine(AutoPopLoop());
    }

    void OnDisable()
    {
        if (popLoopCo != null) { StopCoroutine(popLoopCo); popLoopCo = null; }
        if (popCo != null) { StopCoroutine(popCo); popCo = null; }
        // reset transforms so prefabs don’t stay stretched
        rt.localScale = baseScale;
        var e = rt.localEulerAngles; e.z = baseRotZ; rt.localEulerAngles = e;
        isPopping = false; popT = 0f;
    }

    void Update()
    {
        if (!gameObject.activeInHierarchy) return;

        float t = useUnscaledTime ? Time.unscaledTime : Time.time;

        // rotation wobble
        float rot = baseRotZ;
        if (wobbleRotation)
            rot += Mathf.Sin((t + rotPhase) * rotFrequency * Mathf.PI * 2f) * rotAmplitude;

        // scale wobble (percent of base)
        float scaleMul = 1f;
        if (wobbleScale)
            scaleMul += Mathf.Sin((t + scalePhase) * scaleFrequency * Mathf.PI * 2f) * scaleAmplitude;

        // pop additive
        if (isPopping) scaleMul += popT * popScaleAdd;

        rt.localEulerAngles = new Vector3(rt.localEulerAngles.x, rt.localEulerAngles.y, rot);
        rt.localScale = baseScale * scaleMul;
    }

    IEnumerator AutoPopLoop()
    {
        // small desync
        yield return new WaitForSeconds(Random.Range(0f, 0.4f));
        while (enabled && gameObject.activeInHierarchy)
        {
            float wait = Random.Range(popIntervalRange.x, popIntervalRange.y);
            float t = 0f;
            while (t < wait && enabled && gameObject.activeInHierarchy)
            {
                t += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                yield return null;
            }
            if (!enabled || !gameObject.activeInHierarchy) break;
            TriggerPop();
        }
        popLoopCo = null;
    }

    public void TriggerPop()
    {
        if (!gameObject.activeInHierarchy) return;
        if (popCo != null) StopCoroutine(popCo);
        popCo = StartCoroutine(PopOnce());
    }

    IEnumerator PopOnce()
    {
        isPopping = true;

        // fast up (60% of duration)
        float upTime = popDuration * 0.6f;
        float t = 0f;
        while (t < upTime && enabled && gameObject.activeInHierarchy)
        {
            t += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            float u = Mathf.Clamp01(t / upTime);
            // ease out (snappy)
            popT = 1f - Mathf.Pow(1f - u, 3f);
            yield return null;
        }

        // smooth settle down
        float downTime = popDuration - upTime;
        t = 0f;
        while (t < downTime && enabled && gameObject.activeInHierarchy)
        {
            t += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            float u = Mathf.Clamp01(t / downTime);
            // ease in-out to 0
            popT = 1f - (u * u * (3f - 2f * u));
            yield return null;
        }

        popT = 0f;
        isPopping = false;
        popCo = null;
    }
}
