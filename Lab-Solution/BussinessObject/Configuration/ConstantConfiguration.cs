using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Configuration
{
    internal class ConstantConfiguration
    {
        public class Role
        {
            public static readonly string ADMIN_ID = Guid.NewGuid().ToString();
            public static readonly string USER_ID = Guid.NewGuid().ToString();
        }

        public class Admin
        {
            public static readonly string ID = Guid.NewGuid().ToString();
        }
    }
}
