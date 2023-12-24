using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoPlay : MonoBehaviour
{
    [SerializeField] GameObject activeObject;
    [SerializeField] GameObject inactiveObject;

    public void Toggle()
    {
        activeObject.SetActive(!activeObject.activeSelf);
        inactiveObject.SetActive(!inactiveObject.activeSelf);
    }
}
