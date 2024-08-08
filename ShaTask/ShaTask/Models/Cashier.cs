using System;
using System.Collections.Generic;

namespace ShaTask.Models;

public partial class Cashier
{
    public int Id { get; set; }

    public string CashierName { get; set; }

    public int BranchId { get; set; }

    public virtual Branch Branch { get; set; }

    public virtual ICollection<InvoiceHeader> InvoiceHeaders { get; set; } = new List<InvoiceHeader>();
}
