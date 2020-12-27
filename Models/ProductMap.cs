using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace netcore_nhibernate.Models
{
    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id, x => {
                x.Generator(Generators.Increment);
            });

            Property(b => b.Name, x => {
                x.Length(520);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });

            Property(b => b.Quantity, x => {
                x.Type(NHibernateUtil.Int32);
                x.NotNullable(true);
            });

            Property(b => b.Price, x => {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.NotNullable(true);
            });

            Table("Products");
        }
    }
}