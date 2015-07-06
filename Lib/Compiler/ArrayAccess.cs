//--------------------------------------------------------------------------------------
// Copyright 2015 - Alberto Alonso
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpExpressions.Compiler
{
    static class ArrayAccess
    {
        public static bool access(Queue<Instruction> instructions, Stack<Entry> work, Type target, out Entry result)
        {
            PropertyInfo itemProperty = null;
            MethodInfo itemGetMethod = null;
            int numParams;

            if (target.IsArray)
            {
                numParams = target.GetArrayRank();
            }
            else if ((itemProperty = target.GetProperty("Item")) != null)
            {
                itemGetMethod = itemProperty.GetGetMethod();
                numParams = itemGetMethod.GetParameters().Length;
            }
            else
            {
                throw new CompilerException("Trying to access " + target.FullName + " which is not an array or doesn't have the [] operator overloaded");
            }

            if (itemGetMethod != null)
            {
                // Treat it as a function call
                Entry[] parameters = makeParameters(work, numParams);
                return MethodInvoke.invoke(instructions, itemGetMethod, false, parameters, out result);
            }
            else
            {
                // Treat it as an array
                Instruction instruction = new Instruction();
                instruction.numOperands = numParams + 1;                
                instructions.Enqueue(instruction);
            }

            result = new Entry();
            return true;
        }

        private static Entry[] makeParameters(Stack<Entry> work, int count)
        {
            Entry[] parameters = new Entry[count];
            for (int i = 0; i < count; ++i)
            {
                parameters[i] = work.Pop();
            }
            return parameters;
        }
    }
}
