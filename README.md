# how-use-enum-route-webapi

1. Проблема матчинга ендпойнтов

В проектах с большим количеством ендпойнтов можно столкнуться с проблемой матчинга (route matching):
``` csharp
The request matched multiple endpoints. Matches: ...
   at Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.ReportAmbiguity(CandidateState[] candidateState)
   at Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.ProcessFinalCandidates(HttpContext httpContext, CandidateState[] candidateState)
   at Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.Select(HttpContext httpContext, CandidateState[] candidateState)
   at Microsoft.AspNetCore.Routing.Matching.DfaMatcher.MatchAsync(HttpContext httpContext)
   at Microsoft.AspNetCore.Routing.EndpointRoutingMiddleware.Invoke(HttpContext httpContext)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware.Invoke(HttpContext context)
```

Частично проблему матчинга можно решить с помощью ограничений маршрута (route constraints) по [ссылке](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-8.0#route-constraints)

В моем случае использовались enum в путях, поэтому решил добавить свое ограничение маршрута по enum.

1. Создаем класс EnumConstraint, наследованный от Microsoft.AspNetCore.Routing.IRouteConstraint, реализацию можно посмотреть в солюшне.

2. добавляем в метод ConfigureServices 
``` csharp
builder.Services.AddRouting(options => options.ConstraintMap.Add("enum", typeof(EnumConstraint)));
```

3. Указываем в пути атрибута HttpGet для контроллера, у которого параметром является enum
``` csharp
[HttpGet($"{{type:enum({nameof(ObjectTypeEnum)})}}")]
public ActionResult GetByType([FromRoute] ObjectTypeEnum type)
{
   ...
}
``` 
