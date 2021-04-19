// This class is responsible for doing checks related with memory
using System;

namespace Devon4Net.Infrastructure.Utils
{
    public static class MemoryChecker
    {
        public static bool IsMemoryAllocatedToVariable<T>(T variableToCheck)
        {
            return !IsMemoryNotAllocatedToVariable<T>(variableToCheck);
        }

        public static bool IsMemoryNotAllocatedToVariable<T>(T variableToCheck)
        {
            return variableToCheck == null;
        }
    }
}
