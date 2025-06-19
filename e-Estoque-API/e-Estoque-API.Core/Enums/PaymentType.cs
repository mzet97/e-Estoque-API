namespace e_Estoque_API.Core.Enums;

public enum PaymentType
{
    Pix = 1,
    Deposit = 2,
    CreditCard = 3,
    DebitCard = 4
}

public static class PaymentTypeHelper
{
    public static int ToInt(this PaymentType paymentType)
    {
        return (int)paymentType;
    }

    public static PaymentType FromInt(int value)
    {
        return (PaymentType)value;
    }

    public static PaymentType FromString(string value)
    {
        return Enum.TryParse<PaymentType>(value, out var result) ? result : PaymentType.Pix;
    }

    public static int FromStringForId(string value)
    {
        var paymentType = FromString(value);
        return paymentType.ToInt();
    }
}