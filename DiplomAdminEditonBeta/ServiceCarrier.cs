//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiplomAdminEditonBeta
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceCarrier
    {
        public int Id { get; set; }
        public int IdService { get; set; }
        public int IdCarrier { get; set; }
        public int Cost { get; set; }
    
        public virtual Carrier Carrier { get; set; }
        public virtual Service Service { get; set; }
    }
}
