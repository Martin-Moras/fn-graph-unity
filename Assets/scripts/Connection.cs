using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public LineRenderer line;
    public DataNode outNode;
    public override void Initiallize()() {
        line = GetComponent<LineRenderer>();
    }
}
