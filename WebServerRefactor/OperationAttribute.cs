using System;

namespace WebServerRefactor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OperationAttribute : Attribute
    {
        public string Operation { get; private set; }

        public OperationAttribute(string operation)
        {
            Operation = operation;
        }
    }
}