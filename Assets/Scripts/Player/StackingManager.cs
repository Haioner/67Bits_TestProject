using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    [SerializeField] private Transform stackPos;
    [SerializeField] private float stackOffset = 1f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxTiltAngle = 15f;

    private List<NPCController> _npcStackList = new List<NPCController>();
    private Vector3 _lastPosition;
    private int _maxStackCount = 3;
    private int _stackCount;

    //Invoke stack change
    public delegate void OnStackChange(int stackCount, int maxStackCount);
    public static event OnStackChange onStackChange;

    private void Start()
    {
        _lastPosition = stackPos.position;
        onStackChange?.Invoke(_stackCount, _maxStackCount);
    }

    private void Update()
    {
        Vector3 movementDirection = stackPos.position - _lastPosition;
        _lastPosition = stackPos.position;

        CalculateNPCRotation(movementDirection);
    }

    public bool AvailableStack()
    {
        return _npcStackList.Count < _maxStackCount;
    }

    public bool HasStackCount() {  return _stackCount > 0; }

    public void AddMaxStack()
    {
        _maxStackCount++;
        onStackChange?.Invoke(_stackCount, _maxStackCount);
    }

    public void RemoveNPCFromStack()
    {
        if (_npcStackList.Count > 0)
        {
            _npcStackList[_npcStackList.Count - 1].ScalePunchDestroy();
            _npcStackList.RemoveAt(_npcStackList.Count - 1);
            _stackCount--;
            onStackChange?.Invoke(_stackCount, _maxStackCount);
        }
    }

    public void AddNPCToStack(NPCController nPCController)
    {
        _npcStackList.Add(nPCController);
        _stackCount++;
        StartCoroutine(MoveNPCToStack(nPCController, _stackCount));
        onStackChange?.Invoke(_stackCount, _maxStackCount);
    }

    private IEnumerator MoveNPCToStack(NPCController nPCController, int index)
    {
        yield return new WaitForSeconds(0.7f);

        if(nPCController != null)
        {
            if (index > 1)
            {
                NPCController previousNPC = _npcStackList[index - 2];
                nPCController.transform.SetParent(previousNPC.transform);
            }
            else
            {
                nPCController.transform.SetParent(stackPos);
            }

            nPCController.ResetPosRot();
            nPCController.transform.localPosition = Vector3.up * stackOffset;
        }

    }

    private void CalculateNPCRotation(Vector3 movementDirection)
    {
        float speed = movementDirection.magnitude / Time.deltaTime;

        int index = 0;
        foreach (NPCController npc in _npcStackList)
        {
            Vector3 localMovementDirection = stackPos.InverseTransformDirection(movementDirection).normalized;

            //Calculate tilt based on speed
            float tiltFactor = Mathf.Lerp(0, maxTiltAngle, speed / rotationSpeed);

            Vector3 targetRotation = -Vector3.Cross(Vector3.up, localMovementDirection) * tiltFactor;
            float oscillation = Mathf.Sin(Time.time * rotationSpeed + index) * tiltFactor * 0.5f;
            Vector3 finalRotation = targetRotation + new Vector3(0, 0, oscillation);

            //Apply rotation using Slerp to smooth the movement
            npc.transform.localRotation = Quaternion.Slerp(npc.transform.localRotation, Quaternion.Euler(finalRotation), Time.deltaTime * rotationSpeed);

            index++;
        }
    }
}
