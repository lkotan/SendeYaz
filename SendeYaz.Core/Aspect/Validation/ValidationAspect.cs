﻿using Castle.DynamicProxy;
using FluentValidation;
using SendeYaz.Core.Messages;
using SendeYaz.Core.Utilities.Interceptors;
using System;
using System.Linq;
using ValidationException = SendeYaz.Core.Exceptions.ValidationException;

namespace SendeYaz.Core.Aspect.Validation
{
    public class ValidationAspect:MethodInterception
    {
        private readonly Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new ValidationException(AspectMessage.WrongValidationType);
            }
            Priority = 2;
            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            if (_validatorType.BaseType == null) return;

            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(x => x.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
