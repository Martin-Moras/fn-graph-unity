using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SpecialNodeManager : I_Manager
{
    public override void Initiallize()
    {
        throw new NotImplementedException();
    }
}

public enum SpecialNodes
{
	AllNodes = 0,
	SaverNode = 1,
	Visable = 2,
	Selected = 3,
	Summerize = 4,
	Category = 5,
}
public enum PatternIdentifierNodes
{
	Text,
	Size,
	Color,
	TextColor,
	Shape,
	RepellForce,
	ConnectionStrength,
	ConnectionLength,

}