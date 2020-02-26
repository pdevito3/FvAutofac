# FvAutofac

This project is to demonstrate an issue when using fluent validation with autofac. The issue seems to be related to which 'suestartup' is used, which can cause the FluentValidationModelValidatorProvider to get called twice.
