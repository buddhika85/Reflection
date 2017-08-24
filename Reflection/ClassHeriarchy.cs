using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    class A
    {
    }

    class B : A
    { 
    }

    class C : B 
    {
    }

    class D : B
    {
    }

    public class E
    {        
        public int MyProperty { get; set; }
    }
}
