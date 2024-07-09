using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    [SerializeField] private Transform stackPos;
    [SerializeField] private float stackOffset = 1f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxTiltAngle = 15f;

    private Stack<NPCController> npcStackList = new Stack<NPCController>();
    private int stackCount = 0;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = stackPos.position;
    }

    void Update()
    {
        Vector3 movementDirection = stackPos.position - lastPosition;
        lastPosition = stackPos.position;

        CalculateNPCRotation(movementDirection);
    }

    public void AddNPCToStack(NPCController nPCController)
    {
        StartCoroutine(MoveNPCToStack(nPCController, stackCount));
    }

    public void RemoveNPCFromStack()
    {
        if (npcStackList.Count > 0)
        {
            NPCController removedNPC = npcStackList.Pop();
            removedNPC.ResetToStack();
            stackCount--;
        }
    }

    private IEnumerator MoveNPCToStack(NPCController nPCController, int index)
    {
        yield return new WaitForSeconds(1f);
        npcStackList.Push(nPCController);
        stackCount++;

        if (stackCount > 1)
        {
            NPCController previousNPC = npcStackList.ToArray()[1];
            nPCController.transform.SetParent(previousNPC.transform);
        }
        else
        {
            nPCController.transform.SetParent(stackPos);
        }

        nPCController.ResetToStack();
        nPCController.transform.localPosition = Vector3.up * stackOffset;
    }

    private void CalculateNPCRotation(Vector3 movementDirection)
    {
        int index = 0;
        foreach (NPCController npc in npcStackList)
        {
            //Converts the move direction to local space on stack
            Vector3 localMovementDirection = stackPos.InverseTransformDirection(movementDirection).normalized;
            float tiltFactor = Mathf.Lerp(0, maxTiltAngle, (float)(index + 1) / stackCount);

            //Calculate the invert rotation of stack movement
            Vector3 targetRotation = -Vector3.Cross(Vector3.up, localMovementDirection) * tiltFactor;

            //Sin tilts
            float oscillation = Mathf.Sin(Time.time * rotationSpeed + index) * tiltFactor * 0.5f;
            Vector3 finalRotation = targetRotation + new Vector3(0, 0, oscillation);

            //Apply rotation
            npc.transform.localRotation = Quaternion.Slerp(npc.transform.localRotation, Quaternion.Euler(finalRotation), Time.deltaTime * rotationSpeed);

            index++;
        }
    }
}
