//using System.Numerics;
using UnityEngine;
using System.Collections.Generic;

public class ForkPickUp : MonoBehaviour
{
    [Header("Fork Movement Settings")]
    [SerializeField]
    private float forkMoveSpeed = 2f;
    [SerializeField]
    private float forkMaxHeight = 3f;

    // Non-assignable variables
    private readonly List<GameObject> stackedBoxes = new();
    private float startY;

    private void Awake()
    {
        startY = transform.localPosition.y;
    }

    private void Update()
    {
        if (stackedBoxes.Count > 0 && transform.localPosition.y == startY)
        {
            DropBoxes();
        }
    }

    private void DropBoxes()
    {
        for (int i = stackedBoxes.Count - 1; i >= 0; i--)
        {
            stackedBoxes[i].GetComponent<BoxScript>().DetachBox();
            stackedBoxes.RemoveAt(i);
        }
    }

    public void MoveVerticalMovement(bool up)
    {
        float moveSpeed;

        if (up) {
            moveSpeed = forkMoveSpeed;
        } else
        {
            moveSpeed = -forkMoveSpeed;
        }

        Vector3 pos = transform.localPosition;
        pos.y += moveSpeed * Time.deltaTime * 2;
        pos.y = Mathf.Clamp(pos.y, startY, startY + forkMaxHeight);
        transform.localPosition = pos;
    }

    public void TriggerEffect(Collider other)
    {
        //Debug.Log("triggered");
        if (other.CompareTag("Pallet") && transform.localPosition.y > startY + 0.1f && transform.localPosition.y < startY + 0.8f)
        {
            //Debug.Log("enter trigger");
            int nr = stackedBoxes.Count;
            other.gameObject.GetComponent<BoxScript>().AttachBox(this.gameObject, nr);
            stackedBoxes.Add(other.gameObject);
            Debug.Log("stacked boxes: " + stackedBoxes.Count);
        }
    }
}
