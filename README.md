[![NuGet](https://img.shields.io/nuget/v/Patchable.svg)](https://www.nuget.org/packages/Patchable/) [![GitHub license](https://img.shields.io/github/license/cloudfy/Patchable.svg)](LICENSE)
<p></p>

# Patchable

Enables easy patch (partial changes) without using JsonPatch for ASP.NET and ASP.NET Core.

## Summary
- [Quick Start](#quick-start)
- [Introduction](#introduction)

## Quick start
```powershell
PS> Install-Package Patchable
```

## Introduction
Partial changes of entities is a common task when implementing RESTful services in ASP.NET Web API. RESTful services provide the use of [HTTP PATCH](https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods/PATCH) to conduct partial entity updates.

The ```Patchable``` library enables a convenient approach to only patch partial properties whilst also validating the properties of the containing object. In addition, supporting null value handling (was the property supplied, and was it set to null) is solved.

### Example
Follow this example to patch a single item.

```c-sharp
[HttpPatch()]
public IActionResult Patch(Patchable<PatchSampleItem> sampleItem)
{
    var itemToPatch = new PatchSampleItem()
    {
        TextValue = "This is the original text line"
        , GuidValue = Guid.NewGuid()
        , IntegerValue = 123456
        , DateValue = DateTime.UtcNow
    };
    sampleItem.Patch(itemToPatch);

    return Ok(itemToPatch);
}
```

Partial body of request:

```json
{
    "TextValue" : "This is altered"
}
```

Response:

```json
{
    "TextValue" : "This is altered",
    "GuidValue" : "0B99B1D9-94B3-4254-9DC8-FED54C2798A3",
    "IntegerValue" : 123456,
    "DateValue" : "2022-11-16T10:27:07.345059Z"
}
```

# Documentation

## Using Patchable in tiered or CQRS
When using Patchable in a tiered layer application or CQRS scope, add the Nuget on the business layer (tiered) or command side (CQRS). Inheritance will handle the population. It is easy to use Patchable in Entity Framework Core (EFCore) to only map changed properties of a given DbSet<>. This minimized database payload of roundtrips and reduce collissions that create bad data.

## PatchOptions

### Constructor
| Overload | Description |
|:-|-|
| PatchOptions(bool) | Initializes a new instance setting the IgnoreInvalidProperties flag. 

### Properties
| Property | Type | Description |
|:-|-|-|
| IgnoreInvalidProperties | bool | Gets whether to ignore invalid properties in the body. If true, an ArgumentNullException is thrown. |
