namespace e_Estoque_API.Core.Enums;

public enum SaleType
{
    Unitary = 1,
    Recurrent = 2
}

public static class SaleTypeHelper
{
    public static int ToInt(this SaleType saleType)
    {
        return (int)saleType;
    }

    public static SaleType FromInt(int value)
    {
        return (SaleType)value;
    }

    public static SaleType FromString(string value)
    {
        return Enum.TryParse<SaleType>(value, out var result) ? result : SaleType.Unitary;
    }

    public static int FromStringForId(string value)
    {
        var saleType = FromString(value);
        return saleType.ToInt();
    }
}