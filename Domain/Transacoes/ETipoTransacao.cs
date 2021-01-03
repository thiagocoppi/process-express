namespace Domain.Transacoes
{
    public enum ETipoTransacao
    {
        CREDIT = 0,
        DEBIT = 1,
        INT = 2,
        DIV = 3,
        FEE = 4,
        SRVCHG = 5,
        DEP = 6,
        ATM = 7,
        POS = 8,
        XFER = 9,
        CHECK = 10,
        PAYMENT = 11,
        CASH = 12,
        DIRECTDEP = 13,
        DIRECTDEBIT = 14,
        REPEATPMT = 15,
        OTHER = 16
    }
}