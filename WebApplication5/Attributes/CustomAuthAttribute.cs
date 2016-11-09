using System;

namespace WebApplication5.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CustomAuthAttribute : Attribute
    {
    }
}