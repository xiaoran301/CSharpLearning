using System;

namespace TestModifiers
{
    /*
     * 
     *                in struct                             in class
     * const          必须且只能在field声明时赋值              必须且只能在field声明时赋值 
     *
     * readonly       必须在构造中赋值,不能在声明时             可以在声明时，也可以在构造中赋值，且可以多次赋值，
     *               （没有readonly修饰的filed也是如此）       如果都不赋值可以编译通过,但有警告never assigned to,and use default value 0
     */
    struct SBase
    {
        //public const int X;        //   must be initialize, otherwise: [CS0145] A const field requires a value to be provided
        public const int X = 1;      // 
        
        public readonly int Y;       // it's ok
        //public readonly int Y = 1;   // cannot initialize in struct, otherwise: cannot have instance property or field initializers in structs

        public readonly int Z;
        public int w;
        public SBase(int foo=2) // although parameter with default value: 2, but new SBase() will use struct default implicit constructor
        {
            //X = foo;    // const cannot assign in both struct and class, otherwize: [CS0131] The left-hand side of an assignment must be a variable, property or indexer
            Y = foo;    // it's ok
            Z = foo;    // z must be assigned upon exit, otherwise: CS0171] Field 'SBase.Z' must be fully assigned before control is returned to the caller
            w = foo;
        }

        // public SBase() // error: struct cannot contain explicit parameterless constructor 
        // {
        // }
    }

    class CBase
    {
        //public const int X;        //   must be initialize,
        public const int X = 1;        // it's ok
        public readonly int Y = 1;    // it's ok, note: it's not ok in struct
        public readonly int Z;        // warning if z not assigned in constructor,

        public CBase(int foo=2)
        {
            // Z = foo;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==== Test const vs readonly ====");
            var s = new SBase();
            Console.WriteLine("{0},{1},{2}",SBase.X,s.Y,s.Z);
            
            
            var c = new CBase();
            //Console.WriteLine(c.X); // cannot access static constant 'X' in non-static context
            Console.WriteLine("{0},{1},{2}",CBase.X,c.Y,c.Z);
            
        }
    }
}