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
        public readonly Type[] OtherTypeArray;
        public readonly bool[] OtherIsReadOnlyArray;
        public readonly Type[] TableTypeArray;
        public readonly bool[] TableIsReadOnlyArray;

        public SingleLoopTypeAttribute(Type[] typeArray, bool[] isReadOnlyArray) : this(typeArray, isReadOnlyArray, Array.Empty<Type>(), Array.Empty<bool>(), Array.Empty<Type>(), Array.Empty<bool>()) { }

        public SingleLoopTypeAttribute(Type[] typeArray, bool[] isReadOnlyArray, Type[] otherTypeArray, bool[] otherIsReadOnlyArray) : this(typeArray, isReadOnlyArray, otherTypeArray, otherIsReadOnlyArray, Array.Empty<Type>(), Array.Empty<bool>()) { }

        public SingleLoopTypeAttribute(Type[] typeArray, bool[] isReadOnlyArray, Type[] otherTypeArray, bool[] otherIsReadOnlyArray, Type[] tableTypeArray, bool[] tableIsReadOnlyArray)
        {
            TypeArray = typeArray;
            IsReadOnlyArray = isReadOnlyArray;
            OtherTypeArray = otherTypeArray;
            OtherIsReadOnlyArray = otherIsReadOnlyArray;
            TableTypeArray = tableTypeArray;
            TableIsReadOnlyArray = tableIsReadOnlyArray;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SingleLoopMethodAttribute : Attribute
    {
        public readonly IntrinsicsKind Intrinsics;
        public readonly int OuterCount;
        public readonly int OtherCount;

        public SingleLoopMethodAttribute(IntrinsicsKind intrinsics, int outerCount, int otherCount)
        {
            Intrinsics = intrinsics;
            OuterCount = outerCount;
            OtherCount = otherCount;
        }

        public SingleLoopMethodAttribute(IntrinsicsKind kind, int outerCount) : this(kind, outerCount, 0) { }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class CollisionTypeAttribute : Attribute
    {
        public readonly Type[] OuterTypeArray;
        public readonly bool[] OuterIsReadOnlyArray;
        public readonly Type[] InnerTypeArray;
        public readonly bool[] InnerIsReadOnlyArray;
        public readonly Type[] OtherTypeArray;
        public readonly bool[] OtherIsReadOnlyArray;
        public readonly Type[] TableTypeArray;
        public readonly bool[] TableIsReadOnlyArray;

        public CollisionTypeAttribute(Type[] outerTypeArray, bool[] outerIsReadOnlyArray, Type[] innerTypeArray, bool[] innerIsReadOnlyArray) : this(outerTypeArray, outerIsReadOnlyArray, innerTypeArray, innerIsReadOnlyArray, Array.Empty<Type>(), Array.Empty<bool>(), Array.Empty<Type>(), Array.Empty<bool>()) { }

        public CollisionTypeAttribute(Type[] outerTypeArray, bool[] outerIsReadOnlyArray, Type[] innerTypeArray, bool[] innerIsReadOnlyArray, Type[] otherTypeArray, bool[] otherIsReadOnlyArray) : this(outerTypeArray, outerIsReadOnlyArray, innerTypeArray, innerIsReadOnlyArray, otherTypeArray, otherIsReadOnlyArray, Array.Empty<Type>(), Array.Empty<bool>()) { }

        public CollisionTypeAttribute(Type[] outerTypeArray, bool[] outerIsReadOnlyArray, Type[] innerTypeArray, bool[] innerIsReadOnlyArray, Type[] otherTypeArray, bool[] otherIsReadOnlyArray, Type[] tableTypeArray, bool[] tableIsReadOnlyArray)
        {
            OuterTypeArray = outerTypeArray;
            OuterIsReadOnlyArray = outerIsReadOnlyArray;
            InnerTypeArray = innerTypeArray;
            InnerIsReadOnlyArray = innerIsReadOnlyArray;
            OtherTypeArray = otherTypeArray;
            OtherIsReadOnlyArray = otherIsReadOnlyArray;
            TableTypeArray = tableTypeArray;
            TableIsReadOnlyArray = tableIsReadOnlyArray;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CollisionMethodAttribute : Attribute
    {
        public readonly IntrinsicsKind Intrinsics;
        public readonly int OuterCount;
        public readonly int InnerCount;
        public readonly int OtherCount;

        public CollisionMethodAttribute(IntrinsicsKind intrinsics, int outerCount, int innerCount, int otherCount)
        {
            Intrinsics = intrinsics;
            OuterCount = outerCount;
            InnerCount = innerCount;
            OtherCount = otherCount;
        }

        public CollisionMethodAttribute(IntrinsicsKind kind, int outerCount, int innerCount) : this(kind, outerCount, innerCount, 0) { }

        public CollisionMethodAttribute(IntrinsicsKind kind, int outerCount) : this(kind, outerCount, 0, 0) { }
    }

    public enum CollisionFieldKind
    {
        Outer,
        Inner,
        Other,
        Table,
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CollisionCloseMethodAttribute : Attribute
    {
        public readonly IntrinsicsKind Intrinsics;
        public readonly CollisionFieldKind FieldKind;
        public readonly int FieldIndex;
        public readonly string FieldName;

        public CollisionCloseMethodAttribute(IntrinsicsKind intrinsics, CollisionFieldKind fieldKind, int fieldIndex, string fieldName)
        {
            Intrinsics = intrinsics;
            FieldKind = fieldKind;
            FieldIndex = fieldIndex;
            FieldName = fieldName;
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class LoopParameterAttribute : Attribute
    {
        public readonly int FieldIndex;
        public readonly string FieldName;

        public LoopParameterAttribute(int fieldIndex, string fieldName = "")
        {
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

