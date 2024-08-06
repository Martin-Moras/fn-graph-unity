public class TypeListRequirement{
    public string path;
    public TypeListRequirement[] connectedNodeRequirements;
    public TypeListRequirement(string path, TypeListRequirement[] connectedNodeRequirements){
        this.path = path;
        if (connectedNodeRequirements == null) this.connectedNodeRequirements = new TypeListRequirement[0];
        else this.connectedNodeRequirements = connectedNodeRequirements;
    }
}