using ApplicationEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    public class TokenMap : EntityTypeConfiguration<TokenEntity>
    {
        public TokenMap()
        {
            ToTable("Token");
            HasKey(p => p.TokenId);
            Property(p => p.AuthToken);
            Property(p => p.ExpiresOn);
            Property(p => p.IssuedOn);
            HasRequired(p => p.User).WithMany(p => p.Tokens).HasForeignKey(p => p.UserId);
        }
    }
}
