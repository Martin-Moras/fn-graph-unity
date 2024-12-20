using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public LineRenderer line;
    public VisualNode outNode { get; private set; }
    public void Constructor(VisualNode outNode) {
        line = GetComponent<LineRenderer>();
		this.outNode = outNode;
    }
}
