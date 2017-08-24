using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    class Program
    {
        delegate int IntFunc(int x);

        static void Main(string[] args)
        {
            try
            {
                DemoReflection();
                Console.WriteLine("\nString type information via Reflection\n");
                DemoGettingAssemblyInfo(typeof(string));

                Console.WriteLine("\nInheritance\n");
                CheckInheritance(typeof(D), typeof(A));
                CreateInstances();

                DemoUsingDelegates();
                ////DemoNestedHeirarchies(typeof(D));

            }
            catch (Exception)
            {                
                throw;
            }
            

            Console.ReadKey();
        }

        private static void DemoUsingDelegates()
        {
            Delegate staticD = Delegate.CreateDelegate
                                    (typeof(IntFunc), typeof(Program), "Square");
            Delegate instanceD = Delegate.CreateDelegate
                                    (typeof(IntFunc), new Program(), "Cube");
            Console.WriteLine(staticD.DynamicInvoke(3)); // 9
            Console.WriteLine(instanceD.DynamicInvoke(3)); // 27
        }

        static int Square(int x)
        {
            return x * x;
        }

        int Cube(int x)
        {
            return x * x * x;
        }

        private static void CreateInstances()
        {
            E e = (E) Activator.CreateInstance(typeof(E));
            e.MyProperty = 123;

            Console.WriteLine(e.MyProperty);            
        }

        private static void CheckInheritance(Type subType, Type superType)
        {
            // is operator usage
            bool isChild = new D() is A;
            Console.WriteLine(isChild ? "D is subtype of A" : "No inheritance");

            // Reflection alternative to is operator
            isChild = subType.IsInstanceOfType(new A());
            Console.WriteLine(isChild ? subType.FullName + " is subtype of " + superType.FullName : "No inheritance");
        }

        private static void DemoNestedHeirarchies(Type type)
        {
            Console.WriteLine("\nNested Types of " + type.FullName);
            Type[] nestedTypes = type.GetTypeInfo().GetNestedTypes();
            foreach (var item in nestedTypes)
            {
                Console.WriteLine(item.FullName);
            }
        }

        private static void DemoGettingAssemblyInfo(Type type)
        {
            Console.WriteLine(type.Name);
            Console.WriteLine(type.Namespace);
            Console.WriteLine(type.FullName);
            Console.WriteLine(type.BaseType);
            Console.WriteLine(type.Assembly);
            Console.WriteLine(type.IsPublic);
        }       

        private static void DemoReflection()
        {
            DateTime now = DateTime.Now;

            Type t1 = now.GetType();
            Type t2 = typeof(DateTime);

            Console.WriteLine(t1.FullName);
            Console.WriteLine(t2.FullName);

            Type t3 = typeof(DateTime[]); // 1-d Array type
            Type t4 = typeof(DateTime[,]); // 2-d Array type
            Type t5 = typeof(Dictionary<int, int>); // Closed generic type
            Type t6 = typeof(Dictionary<,>); // Unbound generic type

            Console.WriteLine(t3.FullName);
            Console.WriteLine(t4.FullName);
            Console.WriteLine(t5.FullName);
            Console.WriteLine(t6.FullName);

            Type t7 = Assembly.GetExecutingAssembly().GetType("Reflection.Program");
            Console.WriteLine(t7.FullName);
        }
    }
}
