using System;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;

namespace MyAttribute
{
    public enum IntrinsicsKind
    {
        Ordinal,
        Fma,
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class SingleLoopTypeAttribute : Attribute
    {
        public readonly Type[] TypeArray;
        public readonly bool[] IsReadOnlyArray;
        public readonly string NamePrefix;
        public readonly Type[] OtherTypeArray;
        public readonly bool[] OtherIsReadOnlyArray;
        public readonly string[] OtherNameArray;
        public readonly Type[] TableTypeArray;
        public readonly bool[] TableIsReadOnlyArray;
        public readonly string[] TableNameArray;

        public SingleLoopTypeAttribute(Type[] typeArray, bool[] isReadOnlyArray, string namePrefix) : this(typeArray, isReadOnlyArray, namePrefix, Array.Empty<Type>(), Array.Empty<bool>(), Array.Empty<string>()) { }

        public SingleLoopTypeAttribute(Type[] typeArray, bool[] isReadOnlyArray, string namePrefix, Type[] otherTypeArray, bool[] otherIsReadOnlyArray, string[] otherNameArray) : this(typeArray, isReadOnlyArray, namePrefix, otherTypeArray, otherIsReadOnlyArray, otherNameArray, Array.Empty<Type>(), Array.Empty<bool>(), Array.Empty<string>()) { }

        public SingleLoopTypeAttribute(Type[] typeArray, bool[] isReadOnlyArray, string namePrefix, Type[] otherTypeArray, bool[] otherIsReadOnlyArray, string[] otherNameArray, Type[] tableTypeArray, bool[] tableIsReadOnlyArray, string[] tableNameArray)
        {
            TypeArray = typeArray;
            IsReadOnlyArray = isReadOnlyArray;
            NamePrefix = namePrefix;
            OtherTypeArray = otherTypeArray;
            OtherIsReadOnlyArray = otherIsReadOnlyArray;
            OtherNameArray = otherNameArray;
            TableTypeArray = tableTypeArray;
            TableIsReadOnlyArray = tableIsReadOnlyArray;
            TableNameArray = tableNameArray;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class CollisionTypeAttribute : Attribute
    {
        public readonly Type[] OuterTypeArray;
        public readonly bool[] OuterIsReadOnlyArray;
        public readonly string OuterNamePrefix;
        public readonly Type[] InnerTypeArray;
        public readonly bool[] InnerIsReadOnlyArray;
        public readonly string InnerNamePrefix;
        public readonly Type[] OtherTypeArray;
        public readonly bool[] OtherIsReadOnlyArray;
        public readonly string[] OtherNameArray;
        public readonly Type[] TableTypeArray;
        public readonly bool[] TableIsReadOnlyArray;
        public readonly string[] TableNameArray;

        public CollisionTypeAttribute(Type[] outerTypeArray, bool[] outerIsReadOnlyArray, string outerNamePrefix, Type[] innerTypeArray, bool[] innerIsReadOnlyArray, string innerNamePrefix) : this(outerTypeArray, outerIsReadOnlyArray, outerNamePrefix, innerTypeArray, innerIsReadOnlyArray, innerNamePrefix, Array.Empty<Type>(), Array.Empty<bool>(), Array.Empty<string>()) { }

        public CollisionTypeAttribute(Type[] outerTypeArray, bool[] outerIsReadOnlyArray, string outerNamePrefix, Type[] innerTypeArray, bool[] innerIsReadOnlyArray, string innerNamePrefix, Type[] otherTypeArray, bool[] otherIsReadOnlyArray, string[] otherNameArray) : this(outerTypeArray, outerIsReadOnlyArray, outerNamePrefix, innerTypeArray, innerIsReadOnlyArray, innerNamePrefix, otherTypeArray, otherIsReadOnlyArray, otherNameArray, Array.Empty<Type>(), Array.Empty<bool>(), Array.Empty<string>()) { }

        public CollisionTypeAttribute(Type[] outerTypeArray, bool[] outerIsReadOnlyArray, string outerNamePrefix, Type[] innerTypeArray, bool[] innerIsReadOnlyArray, string innerNamePrefix, Type[] otherTypeArray, bool[] otherIsReadOnlyArray, string[] otherNameArray, Type[] tableTypeArray, bool[] tableIsReadOnlyArray, string[] tableNameArray)
        {
            OuterTypeArray = outerTypeArray;
            OuterIsReadOnlyArray = outerIsReadOnlyArray;
            OuterNamePrefix = outerNamePrefix;
            InnerTypeArray = innerTypeArray;
            InnerIsReadOnlyArray = innerIsReadOnlyArray;
            InnerNamePrefix = innerNamePrefix;
            OtherTypeArray = otherTypeArray;
            OtherIsReadOnlyArray = otherIsReadOnlyArray;
            OtherNameArray = otherNameArray;
            TableTypeArray = tableTypeArray;
            TableIsReadOnlyArray = tableIsReadOnlyArray;
            TableNameArray = tableNameArray;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class MethodIntrinsicsKindAttribute : Attribute
    {
        public readonly IntrinsicsKind Intrinsics;

        public MethodIntrinsicsKindAttribute(IntrinsicsKind intrinsics)
        {
            Intrinsics = intrinsics;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CollisionCloseMethodAttribute : Attribute
    {
        public readonly IntrinsicsKind Intrinsics;
        public readonly int FieldIndex;
        public readonly string FieldName;

        public CollisionCloseMethodAttribute(IntrinsicsKind intrinsics, int fieldIndex, string fieldName)
        {
            Intrinsics = intrinsics;
            FieldIndex = fieldIndex;
            FieldName = fieldName;
        }
    }

    public class EightAttribute : Attribute { }

    public class CountableAttribute : Attribute
    {
        public readonly Type[] AdditionalMemberTypes;
        public readonly string[] AdditionalArgumentMemberNames;
        public readonly string[] AdditionalFieldMemberNames;
        public readonly string IsAliveFunctionString;

        public CountableAttribute()
        {
            AdditionalMemberTypes = Array.Empty<Type>();
            AdditionalArgumentMemberNames = Array.Empty<string>();
            AdditionalFieldMemberNames = Array.Empty<string>();
            IsAliveFunctionString = "";
        }

        public CountableAttribute(Type[] additionalMemberTypes, string[] additionalArgumentMemberNames, string[] additionalFieldMemberNames, string isAliveFunctionString)
        {
            AdditionalMemberTypes = additionalMemberTypes;
            AdditionalArgumentMemberNames = additionalArgumentMemberNames;
            AdditionalFieldMemberNames = additionalFieldMemberNames;
            IsAliveFunctionString = isAliveFunctionString;
        }
    }
}

