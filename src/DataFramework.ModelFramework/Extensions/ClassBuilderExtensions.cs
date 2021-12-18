﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
using ModelFramework.Common.Builders;
using ModelFramework.Objects.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassBuilderExtensions
    {
        internal static ClassBuilder FillFrom(this ClassBuilder instance, IDataObjectInfo dataObjectInfo)
            => instance
                .WithPartial()
                .AddMetadata(dataObjectInfo.Metadata.Convert());

        internal static ClassBuilder WithBaseClass(this ClassBuilder instance, Type baseClassType)
            => instance.WithBaseClass(baseClassType.FullName);

        internal static ClassBuilder AddMetadata(this ClassBuilder instance, string name, object value)
            => instance.AddMetadata(new MetadataBuilder().WithName(name).WithValue(value));

        internal static ClassBuilder AddUsings(this ClassBuilder instance, params string[] usings)
            => instance.AddMetadata(usings.Select(x => new MetadataBuilder().WithName(global::ModelFramework.Objects.MetadataNames.CustomUsing).WithValue(x)));

        internal static ClassBuilder AddUsings(this ClassBuilder instance, IEnumerable<string> usings)
            => instance.AddUsings(usings.ToArray());

        internal static ClassBuilder AddInterfaces(this ClassBuilder instance, params Type[] types)
            => instance.AddInterfaces(types.Select(x => x.FullName));

        internal static ClassBuilder AddInterfaces(this ClassBuilder instance, IEnumerable<Type> types)
            => instance.AddInterfaces(types.ToArray());
    }
}
