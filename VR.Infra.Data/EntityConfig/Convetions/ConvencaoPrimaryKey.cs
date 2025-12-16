using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR.Infra.Data.EntityConfig.Convetions
{
    public class ConvencaoPrimaryKey : Convention
    {
        public ConvencaoPrimaryKey()
        {
            this.Properties()
                .Where(p => p.Name == "Id")
                .Configure(p => p.IsKey());
        }
    }
}
