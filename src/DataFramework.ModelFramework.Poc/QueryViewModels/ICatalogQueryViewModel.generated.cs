using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CrossCutting.Data.Abstractions;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using QueryFramework.Abstractions.Builders;

namespace PDC.Net.Core.QueryViewModels
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.QueryViewModelGenerator", @"1.0.0.0")]
    public partial interface ICatalogQueryViewModel : INotifyPropertyChanged
    {
        System.Nullable<int> Limit
        {
            get;
            set;
        }

        System.Nullable<int> Offset
        {
            get;
            set;
        }

        ObservableCollection<IQueryConditionBuilder> Conditions
        {
            get;
        }

        ObservableCollection<IQuerySortOrderBuilder> OrderByFields
        {
            get;
        }

        IReadOnlyCollection<string> PossibleFieldNames
        {
            get;
        }

        IPagedResult<Catalog> ExecuteQuery();

        CatalogQuery CreateQuery();
    }
}

