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
    
    public partial class Point
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }
        public int IdTask { get; set; }
        public int Position { get; set; }
        public string Address { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Task Task { get; set; }
    }
}
