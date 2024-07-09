using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    [SerializeField] private Transform stackPos;
    [SerializeField] private float stackOffset = 1f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxTiltAngle = 15f;

    private Vector3 lastPosition;
    private List<NPCController> npcStackList = new List<NPCController>();
    private int stackCount;
    private int maxStackCount = 3;

    void Start()
    {
        lastPosition = stackPos.position;
    }

    void Update()
    {
        Vector3 movementDirection = stackPos.position - lastPosition;
        lastPosition = stackPos.position;

        CalculateNPCRotation(movementDirection);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F))
            AddMaxStack();

        if (Input.GetKeyDown(KeyCode.G))
            RemoveNPCFromStack();
#endif
    }

    public bool AvailableStack()
    {
        return npcStackList.Count < maxStackCount;
    }

    public void AddMaxStack()
    {
        maxStackCount++;
        Debug.Log(maxStackCount);
    }

    public void RemoveNPCFromStack()
    {
        if (npcStackList.Count > 0)
        {
            Destroy(npcStackList[npcStackList.Count - 1].gameObject);
            npcStackList.RemoveAt(npcStackList.Count - 1);
            stackCount--;
        }
    }

    public void AddNPCToStack(NPCController nPCController)
    {
        npcStackList.Add(nPCController);
        stackCount++;
        StartCoroutine(MoveNPCToStack(nPCController, stackCount));
    }

    private IEnumerator MoveNPCToStack(NPCController nPCController, int index)
    {
        yield return new WaitForSeconds(1f);

        if(nPCController != null)
        {
            if (index > 1)
            {
                NPCController previousNPC = npcStackList[index - 2];
                nPCController.transform.SetParent(previousNPC.transform);
            }
            else
            {
                nPCController.transform.SetParent(stackPos);
            }

            nPCController.ResetToStack();
            nPCController.transform.localPosition = Vector3.up * stackOffset;
        }

    }

    private void CalculateNPCRotation(Vector3 movementDirection)
    {
        float speed = movementDirection.magnitude / Time.deltaTime; //Calculate speed

        int index = 0;
        foreach (NPCController npc in npcStackList)
        {
            //Convert movement direction locally relative to the stack
            Vector3 localMovementDirection = stackPos.InverseTransformDirection(movementDirection).normalized;

            //Calculate tilt based on speed
            float tiltFactor = Mathf.Lerp(0, maxTiltAngle, speed / rotationSpeed);

            //Calculate target rotation based on local movement direction and tilt factor
            Vector3 targetRotation = -Vector3.Cross(Vector3.up, localMovementDirection) * tiltFactor;

            //Apply oscillation to add movement effect
            float oscillation = Mathf.Sin(Time.time * rotationSpeed + index) * tiltFactor * 0.5f;
            Vector3 finalRotation = targetRotation + new Vector3(0, 0, oscillation);

            //Apply rotation using Slerp to smooth the movement
            npc.transform.localRotation = Quaternion.Slerp(npc.transform.localRotation, Quaternion.Euler(finalRotation), Time.deltaTime * rotationSpeed);

            index++;
        }
    }
}
