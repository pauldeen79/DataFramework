using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CrossCutting.Data.Abstractions;
using DataFramework.ModelFramework.Poc.Repositories;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using PDC.Net.Core.QueryBuilders;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Builders;
using QueryFramework.Core.Builders;

namespace PDC.Net.Core.QueryViewModels
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.QueryViewModelGenerator", @"1.0.0.0")]
    public partial class CatalogQueryViewModel : ICatalogQueryViewModel
    {
        public int? Limit
        {
            get
            {
                return _limit;
            }
            set
            {
                _limit = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Limit"));
                }
            }
        }

        public int? Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Offset"));
                }
            }
        }

        public ObservableCollection<IQueryConditionBuilder> Conditions
        {
            get
            {
                return _conditions;
            }
        }

        public ObservableCollection<IQuerySortOrderBuilder> OrderByFields
        {
            get
            {
                return _orderByFields;
            }
        }

        public IReadOnlyCollection<string> PossibleFieldNames
        {
            get
            {
                var extraFields = _extraFieldRepository.FindExtraFieldsByEntityName(nameof(Catalog));
                return ValidFieldNames
                    .Where(x => !x.StartsWith("ExtraField") || extraFields.Any(y => string.Format("ExtraField{0}", y.FieldNumber) == x))
                    .Select(x => ProcessFieldName(x, extraFields))
                    .Where(x => x != null)
                    .ToList();
            }
        }

        public IPagedResult<Catalog> ExecuteQuery()
        {
            return _queryProcessor.FindPaged(CreateQuery());
        }

        public CatalogQuery CreateQuery()
        {
            var queryBuilder = new CatalogQueryBuilder
            {
                Limit = Limit,
                Offset = Offset
            };
            foreach (var condition in Conditions)
            {
                queryBuilder.Conditions.Add
                (
                    new QueryConditionBuilder
                    (
                        new ExtraFieldQueryExpressionBuilder(_extraFieldRepository, nameof(Catalog), condition.Field.Build()).Build(),
                        condition.Operator,
                        condition.Value,
                        condition.OpenBracket,
                        condition.CloseBracket,
                        condition.Combination
                    )
                );
            }
            foreach (var orderByField in OrderByFields)
            {
                queryBuilder.OrderByFields.Add
                (
                    new QuerySortOrderBuilder
                    (
                        new ExtraFieldQueryExpressionBuilder(_extraFieldRepository, nameof(Catalog), orderByField.Field.Build()).Build(),
                        orderByField.Order
                    )
                );
            }
            return queryBuilder.Build();
        }

        private string ProcessFieldName(string fieldName, IReadOnlyCollection<ExtraField> extraFields)
        {
            if (fieldName.StartsWith("ExtraField"))
            {
                return extraFields.FirstOrDefault(x => fieldName == string.Format("ExtraField{0}", x.FieldNumber))?.Name;
            }
            return fieldName;
        }

        public CatalogQueryViewModel(IExtraFieldRepository extraFieldRepository, IQueryProcessor<CatalogQuery, Catalog> queryProcessor)
        {
            _extraFieldRepository = extraFieldRepository;
            _conditions = new ObservableCollection<IQueryConditionBuilder>();
            _orderByFields = new ObservableCollection<IQuerySortOrderBuilder>();
            _queryProcessor = queryProcessor;
        }

        private static readonly string[] ValidFieldNames = new[] { "Id", "Name", "DateCreated", "DateLastModified", "DateSynchronized", "DriveSerialNumber", "DriveTypeCodeType", "DriveTypeCode", "DriveTypeDescription", "DriveTotalSize", "DriveFreeSpace", "Recursive", "Sorted", "StartDirectory", "ExtraField1", "ExtraField2", "ExtraField3", "ExtraField4", "ExtraField5", "ExtraField6", "ExtraField7", "ExtraField8", "ExtraField9", "ExtraField10", "ExtraField11", "ExtraField12", "ExtraField13", "ExtraField14", "ExtraField15", "ExtraField16", "IsExistingEntity", "AllFields" };

        private const int MaxLimit = int.MaxValue;

        private int? _limit;

        private int? _offset;

        private ObservableCollection<IQueryConditionBuilder> _conditions;

        private ObservableCollection<IQuerySortOrderBuilder> _orderByFields;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IQueryProcessor<CatalogQuery, Catalog> _queryProcessor;

        private readonly IExtraFieldRepository _extraFieldRepository;
    }
}

