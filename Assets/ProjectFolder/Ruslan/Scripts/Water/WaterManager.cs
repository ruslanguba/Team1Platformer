using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    [SerializeField] private WaterContainer _waterContainer;
    [SerializeField] private WaterEmitter _waterEmitter;

    private void Start()
    {
        _waterContainer = GetComponentInChildren<WaterContainer>();
        _waterEmitter = GetComponentInChildren<WaterEmitter>();
        FillContainer();
    }

    private void FillContainer()
    {
        _waterEmitter.FillContainer(_waterContainer.GetContainerSize());
    }
}
