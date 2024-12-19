using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public LineRenderer line;
    public VisualNode outNode;
    public void Initiallize() {
        line = GetComponent<LineRenderer>();
    }
}
