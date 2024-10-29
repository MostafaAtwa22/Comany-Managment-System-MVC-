using Comany_Managment_System_MVC_.Repository.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Services.Attributes
{
    public class UniqueAttribute : ValidationAttribute
    {
        private readonly Type _entityType;
        private readonly string _propertyName;
        private readonly string _idPropertyName;

        public UniqueAttribute(Type entityType, string propertyName, string idPropertyName = "Id")
        {
            _entityType = entityType;
            _propertyName = propertyName;
            _idPropertyName = idPropertyName;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success!;

            var _context = validationContext.GetService<ApplicationDbContext>();

            if (_context is null)
                throw new InvalidOperationException("Database context not available.");

            string? valueString = value.ToString()?.ToLower();

            bool exists = IsValueTaken(_context, valueString, validationContext.ObjectInstance);

            if (exists)
                return new ValidationResult($"The {_propertyName} must be unique.");

            return ValidationResult.Success!;
        }

        private bool IsValueTaken(ApplicationDbContext context, string? value, object viewModel)
        {
            var dbSet = context.GetType().GetProperty(_entityType.Name + "s")?.GetValue(context);

            if (dbSet is IQueryable<object> queryable)
            {
                var parameter = Expression.Parameter(_entityType, "x");
                var property = Expression.Property(parameter, _propertyName);
                var valueExpression = Expression.Constant(value);
                var equalsExpression = Expression.Equal(property, valueExpression);

                var idProperty = viewModel.GetType().GetProperty(_idPropertyName);
                if (idProperty != null)
                {
                    var idValue = idProperty.GetValue(viewModel);
                    if (idValue != null)
                    {
                        var idPropertyExpression = Expression.Property(parameter, _idPropertyName);
                        var idEqualsExpression = Expression.Equal(idPropertyExpression, Expression.Constant(idValue));

                        var combinedExpression = Expression.AndAlso(equalsExpression, Expression.Not(idEqualsExpression));
                        var editLambda = Expression.Lambda(combinedExpression, parameter);

                        var anyMethod = typeof(Queryable).GetMethods()
                            .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                            .MakeGenericMethod(_entityType);

                        return (bool)anyMethod.Invoke(null, new object[] { queryable, editLambda })!;
                    }
                }

                var createLambda = Expression.Lambda(equalsExpression, parameter);

                var anyMethodForCreate = typeof(Queryable).GetMethods()
                    .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(_entityType);

                return (bool)anyMethodForCreate.Invoke(null, new object[] { queryable, createLambda })!;
            }

            return false;
        }
    }
}