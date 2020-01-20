using System;

namespace AutoMapperRegister
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MapperConfiguratorAttribute : Attribute
    {
    }
}
