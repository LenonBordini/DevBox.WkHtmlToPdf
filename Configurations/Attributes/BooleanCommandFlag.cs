namespace DevBox.WkHtmlToPdf.Configurations.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class BooleanCommandFlag : Attribute
{
    public BooleanCommandFlag(string trueFlag, string trueAlias = null, string falseFlag = null, string falseAlias = null)
    {
        TrueFlag = trueFlag;
        TrueAlias = trueAlias;
        FalseFlag = falseFlag;
        FalseAlias = falseAlias;
    }

    public string TrueFlag { get; }
    public string TrueAlias { get; }
    public string FalseFlag { get; }
    public string FalseAlias { get; }

    public string GetTrueFlag()
    {
        return TrueAlias ?? TrueFlag;
    }

    public string GetFalseFlag()
    {
        return FalseAlias ?? FalseFlag;
    }
}
