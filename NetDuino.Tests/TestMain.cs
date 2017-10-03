using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetDuino.Tests
{
    public class TestMain
    {
        public static IPrincipal SetUser()
        {
            var identity = new GenericIdentity("Hasse");
            var principal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = principal;
            return principal;
        }
    }
}
