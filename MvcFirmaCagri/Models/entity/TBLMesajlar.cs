//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcFirmaCagri.Models.entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLMesajlar
    {
        public int ID { get; set; }
        public Nullable<int> Gonderen { get; set; }
        public Nullable<int> Alici { get; set; }
        public string Konu { get; set; }
        public string Mesaj { get; set; }
        public System.DateTime Tarih { get; set; }
        public Nullable<bool> Durum { get; set; }
    
        public virtual TBLFirmalar TBLFirmalar { get; set; }
        public virtual TBLFirmalar TBLFirmalar1 { get; set; }
    }
}
