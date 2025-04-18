namespace DevBox.WkHtmlToPdf.Configurations.Attributes;

[AttributeUsage(AttributeTargets.Property)]
internal class CommandFlag : Attribute
{
    internal CommandFlag(string flag, string alias = null)
    {
        Flag = flag;
        Alias = alias;
    }

    public string Flag { get; }
    public string Alias { get; }

    public string GetFlag()
    {
        return Alias ?? Flag;
    }
}
