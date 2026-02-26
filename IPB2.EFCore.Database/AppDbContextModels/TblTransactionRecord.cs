using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblTransactionRecord
{
    public string TransactionId { get; set; } = null!;

    public string TxnId { get; set; } = null!;

    public string FromMobileNo { get; set; } = null!;

    public string ToMobileNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Message { get; set; }

    public DateTime Timestamp { get; set; }
}
