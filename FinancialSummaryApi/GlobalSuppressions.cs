// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>", Scope = "member", Target = "~M:FinancialSummaryApi.V1.ExceptionMiddleware.Invoke(Microsoft.AspNetCore.Http.HttpContext)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>", Scope = "member", Target = "~M:FinancialSummaryApi.V1.Infrastructure.DynamoDbInitilisationExtensions.ConfigureDynamoDB(Microsoft.Extensions.DependencyInjection.IServiceCollection)")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>", Scope = "member", Target = "~M:FinancialSummaryApi.V1.Controllers.StatementController.Index~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Design", "CA1058:Types should not extend certain base types", Justification = "<Pending>", Scope = "type", Target = "~T:FinancialSummaryApi.V1.Exceptions.Models.ValidationErrorCollection")]
[assembly: SuppressMessage("Design", "CA1010:Generic interface should also be implemented", Justification = "<Pending>", Scope = "type", Target = "~T:FinancialSummaryApi.V1.Exceptions.Models.ValidationErrorCollection")]
