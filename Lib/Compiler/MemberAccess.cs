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
    static class MemberAccess
    {
        public static void identifierAccess(Queue<Instruction> instructions, Registry registry, string identifier, out Entry result)
        {
            object value;
            Type type;

            if (registry.identifiers.TryGetValue(identifier, out value))
            {
                Entry.Type entryType = Entry.fromSystemType(value.GetType());
                Instruction instruction = new Instruction();
                switch (entryType)
                {
                    case Entry.Type.Boolean:
                    {
                        bool bValue = (bool)value;
                        instruction.execute = (Value[] v, ref Value res) => res.boolValue = bValue;
                        break;
                    }
                    case Entry.Type.Double:
                    {
                        double dValue = Convert.ToDouble(value);
                        instruction.execute = (Value[] v, ref Value res) => res.doubleValue = dValue;
                        break;
                    }
                    case Entry.Type.String:
                    {
                        string sValue = value as string;
                        instruction.execute = (Value[] v, ref Value res) => res.stringValue = sValue;
                        break;
                    }
                    case Entry.Type.Object:
                    {
                        instruction.execute = (Value[] v, ref Value res) => res.objectValue = value;
                        break;
                    }
                }
                
                instructions.Enqueue(instruction);
                if (entryType == Entry.Type.Object)
                {
                    result = new Entry { type = Entry.Type.Type, value = value.GetType(), isStatic = false };
                }
                else
                {
                    result = new Entry { type = entryType, value = value, isConstant = true };
                }
            }

            else if (registry.types.TryGetValue(identifier, out type))
            {
                result = new Entry { type = Entry.Type.Type, value = type, isStatic = true };
            }

            else
            {
                result = new Entry { type = Entry.Type.Identifier, value = identifier };
            }
        }

        public static bool memberAccess(Queue<Instruction> instructions, Registry registry, Entry param0, Entry param1, out bool keepObject, out Entry result)
        {
            if (param1.type != Entry.Type.Identifier)
            {
                throw new CompilerException("Trying to access a field without an identifier");
            }
            string fieldName = (string)param1.value;

            if (param0.type == Entry.Type.Type)
            {
                keepObject = accessType(instructions, param0.value as Type, fieldName, param0.isStatic, out result);
            }
            else if (param0.type == Entry.Type.Identifier)
            {
                throw new CompilerException("Cannot find identifier " + (string)param0.value + " in the registry");
            }
            else
            {
                throw new CompilerException("Trying to access a field from a non-object instance");
            }

            return true;
        }

        private static bool accessType(Queue<Instruction> instructions, Type type, string fieldName, bool isStatic, out Entry result)
        {
            Type fieldType = null;
            FieldInfo fieldInfo = null;
            PropertyInfo propInfo = type.GetProperty(fieldName);
            if (propInfo == null)
            {
                fieldInfo = type.GetField(fieldName);
                if (fieldInfo != null)
                {
                    fieldType = fieldInfo.FieldType;
                }
            }
            else
            {
                fieldType = propInfo.PropertyType;
            }

            if (fieldType != null)
            {
                addAccessorInstruction(instructions, fieldType, isStatic, fieldInfo, propInfo, out result);
                return false;
            }
            else
            {
                // Try finding a method
                MethodInfo methodInfo = type.GetMethod(fieldName);
                if (methodInfo == null)
                {
                    throw new CompilerException("Cannot field the field '" + fieldName + "' in the object");
                }
                result = new Entry { type = Entry.Type.Method, value = methodInfo };
                return !isStatic;
            }
        }

        private static void addAccessorInstruction(Queue<Instruction> instructions, Type type, bool isStatic, FieldInfo fieldInfo, PropertyInfo propInfo, out Entry result)
        {
            if (type == typeof(string))
            {
                instructions.Enqueue(makeAccessorInstruction(typeof(string), isStatic, fieldInfo, propInfo, out result));
            }
            else if (type == typeof(bool))
            {
                instructions.Enqueue(makeAccessorInstruction(typeof(bool), isStatic, fieldInfo, propInfo, out result));
            }
            else if (type == typeof(short) || type == typeof(int) || type == typeof(float) || type == typeof(double))
            {
                instructions.Enqueue(makeAccessorInstruction(typeof(double), isStatic, fieldInfo, propInfo, out result));
            }
            else
            {
                instructions.Enqueue(makeAccessorInstruction(typeof(object), isStatic, fieldInfo, propInfo, out result));
            }
        }

        private static Instruction makeAccessorInstruction(Type type, bool isStatic, FieldInfo fieldInfo, PropertyInfo propInfo, out Entry result)
        {
            bool isProperty = propInfo != null;
            Instruction instruction = new Instruction();
            instruction.numOperands = isStatic ? 0 : 1;

            if (type == typeof(string))
            {
                if (isProperty)
                {
                    var getMethod = propInfo.GetGetMethod();
                    if (isStatic)
                        instruction.execute = (Value[] v, ref Value res) => res.stringValue = getMethod.Invoke(null, null) as string;
                    else
                        instruction.execute = (Value[] v, ref Value res) => res.stringValue = getMethod.Invoke(v[0].objectValue, null) as string;
                }
                else
                {
                    if (isStatic)
                        instruction.execute = (Value[] v, ref Value res) => res.stringValue = fieldInfo.GetValue(null) as string;
                    else
                        instruction.execute = (Value[] v, ref Value res) => res.stringValue = fieldInfo.GetValue(v[0].objectValue) as string;
                }
                result = new Entry { type = Entry.Type.String };
            }
            else if (type == typeof(bool))
            {
                if (isProperty)
                {
                    var getMethod = propInfo.GetGetMethod();
                    if (isStatic)
                        instruction.execute = (Value[] v, ref Value res) => res.boolValue = (bool)getMethod.Invoke(null, null);
                    else
                        instruction.execute = (Value[] v, ref Value res) => res.boolValue = (bool)getMethod.Invoke(v[0].objectValue, null);
                }
                else
                {
                    if (isStatic)
                        instruction.execute = (Value[] v, ref Value res) => res.boolValue = (bool)fieldInfo.GetValue(null);
                    else
                        instruction.execute = (Value[] v, ref Value res) => res.boolValue = (bool)fieldInfo.GetValue(v[0].objectValue);
                }
                result = new Entry { type = Entry.Type.Boolean };
            }
            else if (type == typeof(double))
            {
                if (isProperty)
                {
                    var getMethod = propInfo.GetGetMethod();
                    if (isStatic)
                        instruction.execute = (Value[] v, ref Value res) => res.doubleValue = Convert.ToDouble(getMethod.Invoke(null, null));
                    else
                        instruction.execute = (Value[] v, ref Value res) => res.doubleValue = Convert.ToDouble(getMethod.Invoke(v[0].objectValue, null));
                }
                else
                {
                    if (isStatic)
                        instruction.execute = (Value[] v, ref Value res) => res.doubleValue = Convert.ToDouble(fieldInfo.GetValue(null));
                    else
                        instruction.execute = (Value[] v, ref Value res) => res.doubleValue = Convert.ToDouble(fieldInfo.GetValue(v[0].objectValue));
                }
                result = new Entry { type = Entry.Type.Double };
            }
            else if (type == typeof(object))
            {
                if (isProperty)
                {
                    var getMethod = propInfo.GetGetMethod();
                    if (isStatic)
                        instruction.execute = (Value[] v, ref Value res) => res.objectValue = getMethod.Invoke(null, null);
                    else
                        instruction.execute = (Value[] v, ref Value res) => res.objectValue = getMethod.Invoke(v[0].objectValue, null);
                    result = new Entry { type = Entry.Type.Type, value = propInfo.PropertyType };
                }
                else
                {
                    if (isStatic)
                        instruction.execute = (Value[] v, ref Value res) => res.objectValue = fieldInfo.GetValue(null);
                    else
                        instruction.execute = (Value[] v, ref Value res) => res.objectValue = fieldInfo.GetValue(v[0].objectValue);
                        
                    result = new Entry { type = Entry.Type.Type, value = fieldInfo.FieldType };
                }
            }
            else
            {
                throw new CompilerException("Internal compiler error: Unknown type for the member accessor field: " + type.FullName);
            }

            return instruction;
        }
    }
}
