using ApplicationEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    public class UserMap:EntityTypeConfiguration<UserEntity>
    {
        public UserMap()
        {
            ToTable("User");
            HasKey(p => p.UserId);
            Property(p => p.UserName);
            Property(p => p.UserPassword);
        }
    }
}
