using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpExpressions.Compiler
{
    static class Accessors
    {
        public static bool memberAccess(Queue<Instruction> instructions, Registry registry, Entry param0, Entry param1, out Entry result)
        {
            if (param1.type != Entry.Type.Identifier)
            {
                throw new CompilerException("Trying to access a field without an identifier");
            }

            string fieldName = (string)param1.value;

            if (param0.type == Entry.Type.Identifier)
            {
                object accessed;
                Type type;
                string accessedName = (string)param0.value;

                // Solve the first param from the registry
                if (registry.identifiers.TryGetValue(accessedName, out accessed))
                {
                    Instruction instruction = new Instruction();
                    instruction.execute = (Value[] v, ref Value res) => res.objectValue = accessed;
                    instructions.Enqueue(instruction);

                    accessObject(instructions, accessed, fieldName, out result);
                }
                else if (registry.types.TryGetValue(accessedName, out type))
                {
                    accessType(instructions, type, fieldName, out result);
                }
                else
                {
                    throw new CompilerException("Cannot find identifier " + (string)param0.value + " in the registry");
                }
            }
            else if (param0.type == Entry.Type.Object)
            {
                accessObject(instructions, param0.value, fieldName, out result);
            }
            else
            {
                throw new CompilerException("Trying to access a field from a non-object instance");
            }

            return true;
        }

        private static void accessObject(Queue<Instruction> instructions, object accessed, string fieldName, out Entry result)
        {
            Type type = accessed.GetType();
            Type fieldType;
            FieldInfo fieldInfo = null;
            PropertyInfo propInfo = type.GetProperty(fieldName);
            if (propInfo == null)
            {
                fieldInfo = type.GetField(fieldName);
                if (fieldInfo == null)
                {
                    throw new CompilerException("Cannot field the field '" + fieldName + "' in the object");
                }
                fieldType = fieldInfo.FieldType;
            }
            else
            {
                fieldType = propInfo.PropertyType;
            }

            if (fieldType == typeof(string))
            {
                instructions.Enqueue(makeAccessorInstruction(typeof(string), accessed, fieldInfo, propInfo, out result));
            }
            else if (fieldType == typeof(bool))
            {
                instructions.Enqueue(makeAccessorInstruction(typeof(bool), accessed, fieldInfo, propInfo, out result));
            }
            else if (fieldType == typeof(short) || fieldType == typeof(int) || fieldType == typeof(float) || fieldType == typeof(double))
            {
                instructions.Enqueue(makeAccessorInstruction(typeof(double), accessed, fieldInfo, propInfo, out result));
            }
            else
            {
                instructions.Enqueue(makeAccessorInstruction(typeof(object), accessed, fieldInfo, propInfo, out result));
            }
        }

        private static Instruction makeAccessorInstruction(Type type, object accessed, FieldInfo fieldInfo, PropertyInfo propInfo, out Entry result)
        {
            bool isProperty = propInfo != null;
            Instruction instruction = new Instruction();
            instruction.numOperands = 1;

            if (type == typeof(string))
            {
                if (isProperty)
                    instruction.execute = (Value[] v, ref Value res) => res.stringValue = propInfo.GetGetMethod().Invoke(v[0].objectValue, null) as string;
                else
                    instruction.execute = (Value[] v, ref Value res) => res.stringValue = fieldInfo.GetValue(v[0].objectValue) as string;
                result = new Entry { type = Entry.Type.String };
            }
            else if (type == typeof(bool))
            {
                if (isProperty)
                    instruction.execute = (Value[] v, ref Value res) => res.boolValue = (bool)propInfo.GetGetMethod().Invoke(v[0].objectValue, null);
                else
                    instruction.execute = (Value[] v, ref Value res) => res.boolValue = (bool)fieldInfo.GetValue(v[0].objectValue);
                result = new Entry { type = Entry.Type.Boolean };
            }
            else if (type == typeof(double))
            {
                if (isProperty)
                    instruction.execute = (Value[] v, ref Value res) => res.doubleValue = Convert.ToDouble(propInfo.GetGetMethod().Invoke(v[0].objectValue, null));
                else
                    instruction.execute = (Value[] v, ref Value res) => res.doubleValue = Convert.ToDouble(fieldInfo.GetValue(v[0].objectValue));
                result = new Entry { type = Entry.Type.Double };
            }
            else if (type == typeof(object))
            {
                if (isProperty)
                {
                    instruction.execute = (Value[] v, ref Value res) => res.objectValue = propInfo.GetGetMethod().Invoke(v[0].objectValue, null);
                    result = new Entry { type = Entry.Type.Object, value = propInfo.GetGetMethod().Invoke(accessed, null) };
                }
                else
                {
                    instruction.execute = (Value[] v, ref Value res) => res.objectValue = fieldInfo.GetValue(v[0].objectValue);
                    result = new Entry { type = Entry.Type.Object, value = fieldInfo.GetValue(accessed) };
                }
            }
            else
            {
                throw new CompilerException("Internal compiler error: Unknown type for the member accessor field: " + type.FullName);
            }

            return instruction;
        }

        private static void accessType(Queue<Instruction> instructions, Type type, string fieldName, out Entry result)
        {
            result = new Entry();
        }
    }
}
