//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dostava_WPFEntity.Baza
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DostavaEntities : DbContext
    {
        public DostavaEntities()
            : base("name=DostavaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Artikal> Artikals { get; set; }
        public virtual DbSet<Dostava> Dostavas { get; set; }
        public virtual DbSet<dostavaArtikala> dostavaArtikalas { get; set; }
    }
}
