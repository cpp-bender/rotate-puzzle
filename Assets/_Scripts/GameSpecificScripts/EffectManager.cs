using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager>
{
    [SerializeField] GameObject effectPrefab;
    [SerializeField] float loopTime = 5f;

    //public List<Vector3> EffectPositions { get { return effectPositions; } set { effectPositions = value; } }
    //private List<Vector3> effectPositions = new List<Vector3>();

    private List<Vector3> lastEffectPpositions = new List<Vector3>();
    private List<GameObject> oldEffectsGameObjects = new List<GameObject>();
    private float lastEffectShowTime = 0f;

    private void Start()
    {
        //StartCoroutine(ShowEffects());
    }

    private void Update()
    {
        if (CheckEffectTime())
            ShowEffects(lastEffectPpositions);
    }

    public void ShowEffects(List<Vector3> effectPositions)
    {
        lastEffectShowTime = Time.time;
        lastEffectPpositions = effectPositions;

        DestroyOldEffects();

        foreach (var posiiton in effectPositions.ToArray())
        {
            GameObject effectObject = (GameObject)Instantiate(effectPrefab, posiiton, Quaternion.Euler(270f, 0f, 0f));
            effectObject.transform.parent = transform;
            oldEffectsGameObjects.Add(effectObject);
        }
    }

    /*private IEnumerator ShowEffects()
    {
        foreach (var posiiton in effectPositions.ToArray())
        {
            GameObject effectObject = (GameObject)Instantiate(effectPrefab, posiiton, Quaternion.Euler(270f, 0f, 0f));
            effectObject.transform.parent = transform;
            oldEffects.Add(effectObject);
        }

        yield return new WaitForSeconds(loopTime);
        DestroyOldEffects();
        StartCoroutine(ShowEffects());
    }*/

    private void DestroyOldEffects()
    {
        foreach (var effect in oldEffectsGameObjects.ToArray())
        {
            Destroy(effect);
        }
    }

    private bool CheckEffectTime()
    {
        if((lastEffectShowTime + loopTime) < Time.time)
        {
            return true;
        }

        return false;
    }

}
