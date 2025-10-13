//using System.Numerics;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class S_ForkPickUp : MonoBehaviour
{
    List<GameObject> stackedBoxes = new();

    private float startY;

    [Header("Fork Movement Settings")]
    [SerializeField]
    private float forkMoveSpeed = 2f;
    [SerializeField]
    private float forkMaxHeight = 3f;

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
            stackedBoxes[i].GetComponent<S_BoxScript>().detachBox();
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
        if (other.CompareTag("Pallet") && transform.localPosition.y > startY + 0.1f && transform.localPosition.y < startY + 0.8f)
        {
            Debug.Log("enter trigger");
            int nr = stackedBoxes.Count;
            other.gameObject.GetComponent<S_BoxScript>().attachBox(this.gameObject, nr);
            stackedBoxes.Add(other.gameObject);
        }
    }
}
