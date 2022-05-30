using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpherePoisitionManager : MonoBehaviour
{
    [SerializeField] private List<Transform> boneTransforms;
    private List<(Transform, Coroutine)> _bones;

    private void Awake()
    {
        foreach (Transform bone in boneTransforms)
        {
            StartCoroutine(CheckBoneDesiredLocation(boneTransforms.IndexOf(bone)));
            //_bones.Add((bone, StartCoroutine(CheckBoneDesiredLocation(boneTransforms.IndexOf(bone)))));
        }
    }

    private IEnumerator CheckBoneDesiredLocation(int index)
    {
        while (true)
        {
            Transform boneTransform = boneTransforms[index];
            if (boneTransform)
            {
                Collider[] hitObjects = Physics.OverlapSphere(boneTransform.position, 1f, LayerMask.NameToLayer("Default"));

                if (hitObjects.Length > 0)
                {
                    //trees = trees.OrderBy((d) => (d.position - transform.position).sqrMagnitude).ToArray();
                    List<Collider> orderedObjects = hitObjects.OrderBy(
                        (x) => (x.transform.position - boneTransform.position).sqrMagnitude).ToList();
                    
                    // Debug.DrawLine(transform.position, orderedObjects[0].transform.position);
                    
                    boneTransform.position = orderedObjects[0].ClosestPoint(boneTransform.position);
                }   
            }
            
            yield return new WaitForEndOfFrame();
        }
    }
}
