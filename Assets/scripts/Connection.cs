using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public LineRenderer line;
    public Node outNode;
    private void Awake() {
        line = GetComponent<LineRenderer>();
    }
}
