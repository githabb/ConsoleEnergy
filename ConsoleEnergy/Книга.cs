//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleEnergy
{
    using System;
    using System.Collections.Generic;
    
    public partial class Книга
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Книга()
        {
            this.Корзина_Заказа = new HashSet<Корзина_Заказа>();
        }
    
        public int Код_книги { get; set; }
        public string Раздел_литературы { get; set; }
        public string Название { get; set; }
        public string Авторы { get; set; }
        public Nullable<int> Год_издания { get; set; }
        public double Цена { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Корзина_Заказа> Корзина_Заказа { get; set; }
    }
}
