//using System;
//using DataFramework.ModelFramework.Abstractions;
//using ModelFramework.Common.Contracts;

//namespace DataFramework.ModelFramework
//{
//    public class ModelMetadataNotRenderableAsAttribute : IMetadata, INotRenderableAsAttribute
//    {
//        public string Name { get; }
//        public object? Value { get; }

//        public ModelMetadataNotRenderableAsAttribute(string name, object? value)
//        {
//            if (string.IsNullOrEmpty(name))
//            {
//                throw new ArgumentNullException(nameof(name));
//            }

//            Name = name;
//            Value = value;
//        }

//        public override string ToString() => $"Not renderable: [{Name}] = [{Value}]";
//    }
//}
