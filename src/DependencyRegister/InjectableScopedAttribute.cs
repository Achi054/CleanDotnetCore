using System;

namespace DependencyRegister
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InjectableScopedAttribute : Attribute
    {
    }
}
