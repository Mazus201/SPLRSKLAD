//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPY4et.Resourse
{
    using System;
    using System.Collections.Generic;
    
    public partial class MainTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public Nullable<decimal> CostJa { get; set; }
        public Nullable<decimal> CostRub { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<decimal> SelfCost { get; set; }
        public Nullable<decimal> MarktCost { get; set; }
        public Nullable<decimal> WholeCost { get; set; }
        public Nullable<double> MarginWC { get; set; }
        public Nullable<decimal> RetailCost { get; set; }
        public string Status { get; set; }
    }
}
